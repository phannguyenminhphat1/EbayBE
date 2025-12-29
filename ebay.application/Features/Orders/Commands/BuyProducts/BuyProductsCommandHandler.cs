using System.Net;
using System.Text.Json;
using ebay.application.Features.Orders;
using ebay.domain.Entities;
using MediatR;

public class BuyProductsCommandHandler : IRequestHandler<BuyProductsCommand, ResponseService<object>>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderDetailRepository _orderDetailRepo;

    private readonly IOrderDetailQuery _orderDetailQuery;


    private readonly ICurrentUserService _currentUser;

    public BuyProductsCommandHandler(IOrderRepository orderRepo, IOrderDetailQuery orderDetailQuery, IOrderDetailRepository orderDetailRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _orderRepo = orderRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _orderDetailRepo = orderDetailRepo;
        _orderDetailQuery = orderDetailQuery;


    }
    public async Task<ResponseService<object>> Handle(BuyProductsCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        if (request.Dto.Ids!.Count == 0)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: OrderMessages.ORDER_DETAIL_IDS_ARE_REQUIRED
            );
        }
        var orderDetails = await _orderDetailQuery.GetByIdsWithSeller(request.Dto.Ids);
        if (orderDetails.Count != request.Dto.Ids.Count)
        {
            var foundIds = orderDetails.Select(x => x.Id).ToHashSet();
            var missingIds = request.Dto.Ids.Where(id => !foundIds.Contains(id)).ToList();

            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: @$"{OrderMessages.ORDER_DETAIL_NOT_FOUND} - Order details: {JsonSerializer.Serialize(missingIds)}"
            );
        }

        var cartOrder = await _orderRepo.GetByOrderDetailIds(request.Dto.Ids, userId);
        if (cartOrder == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: OrderMessages.ORDER_NOT_FOUND
            );
        }

        var groupedBySeller = orderDetails.GroupBy(x => x.SellerId);
        foreach (var sellerGroup in groupedBySeller)
        {
            var newOrder = new OrderEntity(userId, OrderStatusEnum.WaitingOwnerConfirmation.ToString());

            foreach (var item in sellerGroup)
            {
                newOrder.AddOrUpdateItem(
                    item.ListingId,
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice
                );
            }
            await _orderRepo.Add(newOrder);
        }

        var purchasedDetailIds = orderDetails.Select(x => x.Id).ToList();
        cartOrder.RemoveOrderDetails(purchasedDetailIds);
        cartOrder.RecalculateTotal();

        await _orderRepo.Update(cartOrder);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: OrderMessages.BUY_SUCCESSFULLY_AND_WAITING_CONFIRMATION
        );
    }
}
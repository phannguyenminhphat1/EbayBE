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

    private readonly ICurrentUserService _currentUser;

    public BuyProductsCommandHandler(IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _orderRepo = orderRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _orderDetailRepo = orderDetailRepo;

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
        var orderDetails = await _orderDetailRepo.GetByIds(request.Dto.Ids);
        if (orderDetails.Count != request.Dto.Ids.Count)
        {
            var foundIds = orderDetails.Select(x => x.Id).ToHashSet();
            var missingIds = request.Dto.Ids.Where(id => !foundIds.Contains(id)).ToList();

            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: @$"{OrderMessages.ORDER_DETAIL_NOT_FOUND} - Order details: {JsonSerializer.Serialize(missingIds)}"
            );
        }

        var order = await _orderRepo.GetByOrderDetailIds(request.Dto.Ids, userId);
        if (order == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: OrderMessages.ORDER_NOT_FOUND
            );
        }

        var totalActiveDetails = order.OrderDetails.Count(x => !x.Deleted!.Value);

        // Mua toàn bộ
        if (totalActiveDetails == orderDetails.Count)
        {
            order.ChangeStatus(OrderStatusEnum.WaitingOwnerConfirmation.ToString());
            await _orderRepo.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.OK,
                message: OrderMessages.BUY_SUCCESSFULLY_AND_WAITING_CONFIRMATION
            );
        }

        // Mua một phần
        var newOrder = new OrderEntity(userId, OrderStatusEnum.WaitingOwnerConfirmation.ToString());

        foreach (var detail in orderDetails)
        {
            newOrder.AddOrUpdateItem(
                detail.ListingId,
                detail.ProductId,
                detail.Quantity,
                detail.UnitPrice
            );
        }
        var detailIds = orderDetails.Select(x => x.Id).ToList();
        order.RemoveOrderDetails(detailIds);
        order.RecalculateTotal();

        await _orderRepo.Update(order);
        await _orderRepo.Add(newOrder);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: OrderMessages.BUY_SUCCESSFULLY_AND_WAITING_CONFIRMATION
        );
    }
}
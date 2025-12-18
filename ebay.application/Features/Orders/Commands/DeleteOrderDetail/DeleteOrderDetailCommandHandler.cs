using System.Net;
using ebay.application.Features.Orders;
using MediatR;

public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, ResponseService<object>>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IOrderDetailRepository _orderDetailRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;
    public DeleteOrderDetailCommandHandler(IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _orderRepo = orderRepo;
        _orderDetailRepo = orderDetailRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }
    public async Task<ResponseService<object>> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
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
                message: OrderMessages.ORDER_DETAIL_NOT_FOUND,
                data: new
                {
                    missing_order_detail_ids = missingIds
                }
            );
        }
        var order = await _orderRepo.GetByOrderDetailIds(request.Dto.Ids);

        if (order == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: OrderMessages.ORDER_NOT_FOUND
            );
        }
        else if (order.Status != OrderStatusEnum.InCart.ToString())
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: OrderMessages.ORDER_STATUS_IS_NOT_IN_CART
            );
        }

        if (order.BuyerId != userId)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: OrderMessages.NOT_YOURS_ORDER
            );
        }
        order.RemoveOrderDetails(request.Dto.Ids);
        await _orderRepo.Update(order);
        await _unitOfWork.SaveChangesAsync();

        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: OrderMessages.ORDER_DETAIL_DELETE_SUCCESSFULLY
        );
    }
}
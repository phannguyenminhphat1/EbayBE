using System.Net;
using ebay.application.Features.Orders;
using MediatR;

public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, ResponseService<object>>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;

    public UpdateOrderDetailCommandHandler(IOrderRepository orderRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _orderRepo = orderRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }
    public async Task<ResponseService<object>> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var orderDetailId = request.Dto.Id!.Value;
        if (!int.TryParse(request.Dto.Quantity, out int quantity))
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.UnprocessableEntity,
                message: CommonMessages.ERROR,
                data: new
                {
                    quantity = OrderMessages.QUANTITY_MUST_BE_A_NUMBER
                }
            );
        }
        if (int.IsNegative(quantity))
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.UnprocessableEntity,
                message: CommonMessages.ERROR,
                data: new
                {
                    quantity = OrderMessages.QUANTITY_MUST_BE_POSITIVE
                }
            );
        }
        var order = await _orderRepo.GetOrderInCartById(userId);
        if (order == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: OrderMessages.ORDER_NOT_FOUND

            );
        }
        try
        {
            order.UpdateOrderDetailQuantity(orderDetailId, quantity);
        }
        catch (Exception ex)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: ex.Message
            );
        }
        await _orderRepo.Update(order);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: OrderMessages.UPDATE_ORDER_SUCCESSFULLY
        );

    }
}
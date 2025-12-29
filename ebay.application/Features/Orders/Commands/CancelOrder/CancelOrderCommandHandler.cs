using System.Net;
using System.Text.Json;
using ebay.application.Features.Orders;
using MediatR;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, ResponseService<string>>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderDetailRepository _orderDetailRepo;

    private readonly ICurrentUserService _currentUser;

    public CancelOrderCommandHandler(IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _orderRepo = orderRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _orderDetailRepo = orderDetailRepo;

    }
    public async Task<ResponseService<string>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        int id = int.Parse(request.Id);
        var order = await _orderRepo.GetOrderById(userId, OrderStatusEnum.WaitingOwnerConfirmation.ToString(), id);
        if (order == null)
        {
            return new ResponseService<string>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: OrderMessages.ORDER_NOT_FOUND
            );
        }
        order.SoftDelete();
        System.Console.WriteLine("---------------------------------------------------------------------");
        System.Console.WriteLine(JsonSerializer.Serialize(order));
        await _orderRepo.Update(order);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<string>(
            statusCode: (int)HttpStatusCode.OK,
            message: OrderMessages.CANCEL_ORDER_SUCCESSFULLY
        );
    }
}
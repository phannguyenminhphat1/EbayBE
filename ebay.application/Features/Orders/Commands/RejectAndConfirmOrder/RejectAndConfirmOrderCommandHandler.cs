using System.Net;
using ebay.application.Features.Orders.Commands;
using ebay.domain.Interfaces;
using MediatR;

public class RejectAndConfirmOrderCommandHandler : IRequestHandler<RejectAndConfirmOrderCommand, ResponseService<object>>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IOrderDetailRepository _orderDetailRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;
    private readonly IUserRepository _userRepo;

    public RejectAndConfirmOrderCommandHandler(IOrderRepository orderRepo, IUserRepository userRepo, IOrderDetailRepository orderDetailRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _orderRepo = orderRepo;
        _orderDetailRepo = orderDetailRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _userRepo = userRepo;
    }
    public async Task<ResponseService<object>> Handle(RejectAndConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var sellerId = _currentUser.UserId;
        int id = int.Parse(request.Id);
        var currentUser = await _userRepo.FindUserById(sellerId);
        if (currentUser == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.USER_NOT_FOUND
            );
        }
        var order = await _orderRepo.GetOrderForSeller(id, sellerId, OrderStatusEnum.WaitingOwnerConfirmation.ToString());
        if (order == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: "Order not found or you do not have permission to process this order"
            );
        }
        if (request.Dto.IsConfirmed)
        {
            order.ChangeStatus(OrderStatusEnum.Completed.ToString());
        }
        else
        {
            order.ChangeStatus(OrderStatusEnum.Canceled.ToString());
        }

        await _orderRepo.Update(order);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: request.Dto.IsConfirmed ? "Order confirmed successfully" : "Order rejected successfully"
        );
    }
}
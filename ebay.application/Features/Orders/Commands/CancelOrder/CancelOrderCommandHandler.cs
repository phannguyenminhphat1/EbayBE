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
        throw new NotImplementedException();
    }
}
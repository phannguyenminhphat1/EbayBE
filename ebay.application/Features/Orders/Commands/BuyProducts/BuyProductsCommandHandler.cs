using System.Net;
using ebay.application.Features.Orders;
using MediatR;

public class BuyProductsCommandHandler : IRequestHandler<BuyProductsCommand, ResponseService<object>>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;

    public BuyProductsCommandHandler(IOrderRepository orderRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _orderRepo = orderRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
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
        throw new Exception("");
    }
}
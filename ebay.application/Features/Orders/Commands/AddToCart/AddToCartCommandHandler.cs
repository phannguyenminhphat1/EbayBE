using System.Net;
using ebay.application.Features.Orders;
using ebay.domain.Entities;
using MediatR;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, ResponseService<object>>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IListingRepository _listingRepo;
    private readonly ICurrentUserService _currentUser;
    public AddToCartCommandHandler(IOrderRepository orderRepo, IListingRepository listingRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _orderRepo = orderRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _listingRepo = listingRepo;
    }
    public async Task<ResponseService<object>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var listingId = request.Dto.ListingId!.Value;

        var listing = await _listingRepo.GetById(listingId);
        if (listing == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: OrderMessages.LISTING_NOT_FOUND
            );
        }

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
            order = new OrderEntity(userId, OrderStatusEnum.InCart.ToString());
            order.AddOrUpdateItem(listingId, listing.ProductId!.Value, quantity, listing.StartingPrice!.Value);
            await _orderRepo.Add(order);
        }
        else
        {
            order.AddOrUpdateItem(listingId, listing.ProductId!.Value, quantity, listing.StartingPrice!.Value);
            await _orderRepo.Update(order);
        }
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: OrderMessages.ADD_TO_CART_SUCCESSFULLY
        );
    }
}
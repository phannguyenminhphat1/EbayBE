using System.Net;
using ebay.application.Features.Orders;
using ebay.domain.Entities;
using MediatR;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, ResponseService<object>>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IOrderDetailRepository _orderDetailRepo;
    private readonly IProductRepository _productRepo;
    private readonly IUnitOfWork _unitOfWork;

    private readonly ICurrentUserService _currentUser;
    public AddToCartCommandHandler(IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo, IProductRepository productRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _orderRepo = orderRepo;
        _orderDetailRepo = orderDetailRepo;
        _productRepo = productRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }
    public async Task<ResponseService<object>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var productId = request.Dto.ProductId!.Value;

        var product = await _productRepo.GetById(productId);
        if (product == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: ProductMessages.PRODUCT_NOT_FOUND
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
            order.AddOrUpdateItem(productId, quantity, product.Price!.Value);
            await _orderRepo.Add(order);
        }
        else
        {
            order.AddOrUpdateItem(productId, quantity, product.Price!.Value);
            await _orderRepo.Update(order);
        }
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: OrderMessages.ADD_TO_CART_SUCCESSFULLY
        );
    }
}
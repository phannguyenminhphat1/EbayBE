using System.Net;
using System.Text.Json;
using ebay.application.Features.Orders;
using ebay.application.Features.Orders.Queries;
using ebay.application.Interfaces;
using ebay.domain.Interfaces;
using MediatR;

public class GetOrdersBySellerQueryHandler : IRequestHandler<GetOrdersBySellerQuery, ResponseService<ResponsePagedService<object>>>
{
    private readonly IUserRepository _userRepo;
    private readonly ICurrentUserService _currentUser;

    private readonly IGetOrdersBySellerQuery _ordersBySellerQuery;
    public GetOrdersBySellerQueryHandler(IGetOrdersBySellerQuery ordersBySellerQuery, IUserRepository userRepo, ICurrentUserService currentUser)
    {
        _userRepo = userRepo;
        _ordersBySellerQuery = ordersBySellerQuery;
        _currentUser = currentUser;
    }
    public async Task<ResponseService<ResponsePagedService<object>>> Handle(GetOrdersBySellerQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var currentUser = await _userRepo.FindUserById(userId);
        if (currentUser == null)
        {
            return new ResponseService<ResponsePagedService<object>>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.USER_NOT_FOUND
            );
        }
        var status = string.IsNullOrEmpty(request.Dto.Status) ? OrderStatusEnum.WaitingOwnerConfirmation.ToString() : request.Dto.Status;
        // PAGE & PAGESIZE
        int page = int.Parse(request.PaginationDto.Page!);
        int pageSize = int.Parse(request.PaginationDto.PageSize!);

        if (status != OrderStatusEnum.WaitingOwnerConfirmation.ToString() && status != OrderStatusEnum.Completed.ToString() && status != OrderStatusEnum.Canceled.ToString())
        {
            return new ResponseService<ResponsePagedService<object>>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: $@"Order is invalid, Order must be: WaitingOwnerConfirmation, Completed, Canceled"
            );
        }

        var (orders, totalRecords) = await _ordersBySellerQuery.GetOrdersBySeller(userId, status, page, pageSize);
        int totalPage = (int)Math.Ceiling((double)totalRecords / pageSize);

        var result = orders.Select(o => new GetOrderBySellerDto
        {
            OrderId = o.OrderId,
            TotalAmount = o.TotalAmount,
            OrderStatus = o.OrderStatus,
            OrderCreatedAt = o.OrderCreatedAt,
            BuyerId = o.BuyerId,
            Buyer = new GetOrderBuyerDto
            {
                BuyerAddress = o.BuyerAddress,
                BuyerAva = o.BuyerAva,
                BuyerAvatar = o.BuyerAvatar,
                BuyerEmail = o.BuyerEmail,
                BuyerFullName = o.BuyerFullName,
                BuyerPhone = o.BuyerPhone
            },
            Seller = new GetOrderSellerDto
            {
                SellerId = o.SellerId,
                SellerAddress = o.SellerAddress,
                SellerAva = o.SellerAva,
                SellerAvatar = o.SellerAvatar,
                SellerEmail = o.SellerEmail,
                SellerFullName = o.SellerFullName,
                SellerPhone = o.SellerPhone
            },
            OrderDetails = string.IsNullOrEmpty(o.OrderDetails) ? [] : JsonSerializer.Deserialize<List<GetOrderDetailsDto>>(o.OrderDetails)?.Select(od => new GetOrderDetailsDto
            {
                OrderDetailId = od.OrderDetailId,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                CreatedAt = od.CreatedAt,
                Listing = new ListingDto
                {
                    ListingTitle = od.Listing!.ListingTitle,
                    ListingStatus = od.Listing.ListingStatus,
                    ListingCreatedAt = od.Listing.ListingCreatedAt,
                },
                Product = new ProductDto
                {
                    ProductId = od.Product!.ProductId,
                    ProductName = od.Product.ProductName,
                    Description = od.Product.Description,
                    Stock = od.Product.Stock,
                    ProductImages = od.Product.ProductImages.Select(pi => new ProductImageDto
                    {
                        Id = pi.Id,
                        ImageUrl = pi.ImageUrl
                    }).ToList()
                },
            }).ToList()
        }).ToList();

        var data = new ResponsePagedService<object>(
            data: new
            {
                orders = result
            },
            pagination: new Pagination(page, pageSize, totalRecords, totalPage)
        );

        return new ResponseService<ResponsePagedService<object>>(
            statusCode: (int)HttpStatusCode.OK,
            message: OrderMessages.GET_ORDERS_SUCCESSFULLY,
            data: data
        );
    }
}
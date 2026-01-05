using System.Net;
using System.Text.Json;
using AutoMapper;
using ebay.application.Features.Orders;
using ebay.application.Interfaces;
using ebay.domain.Interfaces;
using MediatR;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ResponseService<ResponsePagedService<object>>>
{
    private readonly IUserRepository _userRepo;
    private readonly ICurrentUserService _currentUser;

    private readonly IOrdersListingDetailRepository _ordersListingDetailRepo;
    public GetOrdersQueryHandler(IOrdersListingDetailRepository ordersListingDetailRepo, IUserRepository userRepo, ICurrentUserService currentUser)
    {
        _userRepo = userRepo;
        _ordersListingDetailRepo = ordersListingDetailRepo;
        _currentUser = currentUser;
    }
    public async Task<ResponseService<ResponsePagedService<object>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
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
        var status = string.IsNullOrEmpty(request.Dto.Status) ? OrderStatusEnum.All.ToString() : request.Dto.Status;
        // PAGE & PAGESIZE
        int page = int.Parse(request.PaginationDto.Page!);
        int pageSize = int.Parse(request.PaginationDto.PageSize!);

        if (!EnumValidationStringService.IsValidName<OrderStatusEnum>(status))
        {
            return new ResponseService<ResponsePagedService<object>>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: $@"Order is invalid, Order must be: {EnumValidationStringService.GetValidEnumNames<OrderStatusEnum>()}"
            );
        }

        var (orders, totalRecords) = await _ordersListingDetailRepo.GetOrdersListingDetail(userId, status, page, pageSize);
        int totalPage = (int)Math.Ceiling((double)totalRecords / pageSize);

        var result = orders.Select(o => new OrdersListingDetailDto
        {
            OrderId = o.OrderId,
            TotalAmount = o.TotalAmount,
            OrderStatus = o.OrderStatus,
            OrderCreatedAt = o.OrderCreatedAt,
            Buyer = new BuyerDto
            {
                UserId = o.UserId,
                FullName = o.FullName,
                Email = o.Email,
                Avatar = o.Avatar
            },
            OrderDetails = string.IsNullOrEmpty(o.OrderDetails) ? [] : JsonSerializer.Deserialize<List<OrdersDetailListingDetailReadModel>>(o.OrderDetails)?.Select(od => new OrderDetailsDto
            {
                OrderDetailId = od.OrderDetailId,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                CreatedAt = od.CreatedAt,
                Listing = new ListingDto
                {
                    ListingTitle = od.ListingTitle,
                    ListingStatus = od.ListingStatus,
                    ListingCreatedAt = od.ListingCreatedAt,
                },
                Seller = new SellerDto
                {
                    SellerUserId = od.SellerUserId,
                    SellerFullName = od.SellerFullName,
                    SellerEmail = od.SellerEmail,
                    SellerAvatar = od.SellerAvatar,
                    SellerAddress = od.SellerAddress,
                    SellerPhone = od.SellerPhone,
                },
                Product = new ProductDto
                {
                    ProductId = od.ProductId,
                    ProductName = od.ProductName,
                    Description = od.Description,
                    Stock = od.Stock,
                    ProductImages = od.ProductImages.Select(pi => new ProductImageDto
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
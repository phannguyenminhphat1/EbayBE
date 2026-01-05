using MediatR;

namespace ebay.application.Features.Orders.Queries;

public record GetOrdersBySellerQuery(GetOrdersQueryDto Dto, PaginationDto PaginationDto) : IRequest<ResponseService<ResponsePagedService<object>>>;
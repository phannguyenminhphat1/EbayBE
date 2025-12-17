using MediatR;

namespace ebay.application.Features.Orders;

public record GetOrdersQuery(int UserId, GetOrdersQueryDto Dto, PaginationDto PaginationDto) : IRequest<ResponseService<ResponsePagedService<object>>>;
using MediatR;

namespace ebay.application.Features.Orders;

public record GetOrdersQuery(GetOrdersQueryDto Dto, PaginationDto PaginationDto) : IRequest<ResponseService<ResponsePagedService<object>>>;
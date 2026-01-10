using MediatR;

namespace ebay.application.Features.Orders;

public record GetOrderStatisticsQuery(OrderStatisticsQueryDto Dto) : IRequest<ResponseService<List<GetOrderStatisticsDto>>>;
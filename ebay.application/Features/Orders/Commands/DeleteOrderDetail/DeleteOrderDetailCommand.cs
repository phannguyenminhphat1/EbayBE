using MediatR;

namespace ebay.application.Features.Orders;

public record DeleteOrderDetailCommand(DeleteOrderDetailDto Dto) : IRequest<ResponseService<object>>;
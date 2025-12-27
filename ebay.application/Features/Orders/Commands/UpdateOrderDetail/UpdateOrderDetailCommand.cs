using MediatR;

namespace ebay.application.Features.Orders;

public record UpdateOrderDetailCommand(UpdateOrderDetailDto Dto) : IRequest<ResponseService<object>>;
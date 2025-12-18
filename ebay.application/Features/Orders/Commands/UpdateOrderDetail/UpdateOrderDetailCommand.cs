using MediatR;

namespace ebay.application.Features.Orders;

public record UpdateOrderDetailCommand(int UserId, UpdateOrderDetailDto Dto) : IRequest<ResponseService<object>>;
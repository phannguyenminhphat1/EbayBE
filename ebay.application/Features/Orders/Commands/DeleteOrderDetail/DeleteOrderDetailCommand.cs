using MediatR;

namespace ebay.application.Features.Orders;

public record DeleteOrderDetailCommand(int UserId, DeleteOrderDetailDto Dto) : IRequest<ResponseService<object>>;
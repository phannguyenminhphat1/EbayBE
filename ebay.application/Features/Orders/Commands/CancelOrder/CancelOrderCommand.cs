using MediatR;
namespace ebay.application.Features.Orders;

public record CancelOrderCommand(int UserId) : IRequest<ResponseService<string>>;
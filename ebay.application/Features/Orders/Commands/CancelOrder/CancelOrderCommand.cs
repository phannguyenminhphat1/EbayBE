using MediatR;
namespace ebay.application.Features.Orders;

public record CancelOrderCommand(string Id) : IRequest<ResponseService<string>>;
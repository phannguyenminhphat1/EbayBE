using MediatR;
namespace ebay.application.Features.Orders;

public record CancelOrderCommand() : IRequest<ResponseService<string>>;
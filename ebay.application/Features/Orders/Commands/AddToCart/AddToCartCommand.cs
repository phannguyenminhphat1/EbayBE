using MediatR;

namespace ebay.application.Features.Orders;

public record AddToCartCommand(AddToCartDto Dto) : IRequest<ResponseService<object>>;
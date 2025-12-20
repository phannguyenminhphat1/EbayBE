using MediatR;

namespace ebay.application.Features.Orders;

public record BuyProductsCommand(BuyProductDto Dto) : IRequest<ResponseService<object>>;
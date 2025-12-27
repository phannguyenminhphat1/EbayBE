using MediatR;

namespace ebay.application.Features.Orders;

public record BuyProductsCommand(BuyProductsDto Dto) : IRequest<ResponseService<object>>;
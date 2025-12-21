using MediatR;

namespace ebay.application.Features.Orders;

public record BuyProductsCommand(int UserId, BuyProductsDto Dto) : IRequest<ResponseService<object>>;
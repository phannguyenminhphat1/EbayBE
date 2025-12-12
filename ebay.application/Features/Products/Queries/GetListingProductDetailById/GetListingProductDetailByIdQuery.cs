using MediatR;

namespace ebay.application.Features.Products;

public record class GetListingProductDetailByIdQuery(string Id) : IRequest<ResponseService<object>>;
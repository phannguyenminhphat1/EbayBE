using MediatR;

namespace ebay.application.Features.Products;

public record class GetListListingProductDetailQuery(PaginationDto PaginationDto, ListingProductDetailQueryDto ListingProductDetailQueryDto) : IRequest<ResponseService<ResponsePagedService<object>>>;
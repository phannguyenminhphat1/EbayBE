using MediatR;

namespace ebay.application.Features.Products;

public record class GetProductListCategoryQuery(PaginationDto PaginationDto) : IRequest<ResponseService<ResponsePagedService<List<ProductListCategoryDto>>>>;
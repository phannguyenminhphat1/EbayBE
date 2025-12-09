using MediatR;

public record class GetProductListCategoryQuery(PaginationDto PaginationDto) : IRequest<ResponseService<ResponsePagedService<List<ProductListCategoryDto>>>>;
using System.Net;
using AutoMapper;
using MediatR;

public class GetProductListCategoryQueryHandler : IRequestHandler<GetProductListCategoryQuery, ResponseService<ResponsePagedService<List<ProductListCategoryDto>>>>
{
    private readonly IMapper _mapper;
    private readonly IProductListCategoryRepository _productListCategoryRepository;

    public GetProductListCategoryQueryHandler(IMapper mapper, IProductListCategoryRepository productListCategoryRepository)
    {
        _mapper = mapper;
        _productListCategoryRepository = productListCategoryRepository;
    }
    public async Task<ResponseService<ResponsePagedService<List<ProductListCategoryDto>>>> Handle(GetProductListCategoryQuery request, CancellationToken cancellationToken)
    {
        int page = int.Parse(request.PaginationDto.Page!);
        int pageSize = int.Parse(request.PaginationDto.PageSize!);
        var (lstProductCategory, totalRecords) = await _productListCategoryRepository.GetProductListCategory(page, pageSize);
        int totalPage = (int)Math.Ceiling((double)totalRecords / pageSize);
        var lstProductCategoryMapper = _mapper.Map<List<ProductListCategoryDto>>(lstProductCategory);
        var resultResponse = new ResponsePagedService<List<ProductListCategoryDto>>(
            data: lstProductCategoryMapper,
            pagination: new Pagination(page, pageSize, totalRecords, totalPage)
        );
        return new ResponseService<ResponsePagedService<List<ProductListCategoryDto>>>(
            statusCode: (int)HttpStatusCode.OK,
            message: ProductMessages.GET_PRODUCTS_SUCCESSFULLY,
            data: resultResponse
        );
        throw new NotImplementedException();
    }
}
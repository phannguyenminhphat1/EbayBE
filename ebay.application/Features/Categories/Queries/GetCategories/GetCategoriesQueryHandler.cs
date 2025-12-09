using System.Net;
using AutoMapper;
using MediatR;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ResponseService<List<CategoryDto>>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }
    public async Task<ResponseService<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categoryEntities = await _categoryRepository.GetAllCategories();
        var categoriesMapper = _mapper.Map<List<CategoryDto>>(categoryEntities);
        return new ResponseService<List<CategoryDto>>(
            statusCode: (int)HttpStatusCode.OK,
            message: CategoryMessages.GET_CATEGORIES_SUCCESSFULLY,
            data: categoriesMapper
        );
    }
}
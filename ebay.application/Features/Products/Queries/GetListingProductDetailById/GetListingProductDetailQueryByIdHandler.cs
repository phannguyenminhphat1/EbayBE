using System.Net;
using AutoMapper;
using ebay.application.Features.Products;
using MediatR;

public class GetListingProductDetailByIdQueryHandler : IRequestHandler<GetListingProductDetailByIdQuery, ResponseService<object>>
{
    private readonly IMapper _mapper;
    private readonly IListingProductDetailRepository _listingProductDetailRepository;

    private readonly IOptionalCurrentUserService _optionalCurrentUserService;

    public GetListingProductDetailByIdQueryHandler(IMapper mapper, IListingProductDetailRepository listingRepo, IOptionalCurrentUserService optionalCurrentUserService)
    {
        _mapper = mapper;
        _listingProductDetailRepository = listingRepo;
        _optionalCurrentUserService = optionalCurrentUserService;

    }

    public async Task<ResponseService<object>> Handle(GetListingProductDetailByIdQuery request, CancellationToken cancellationToken)
    {
        int id = int.Parse(request.Id);
        int? currentUserId = _optionalCurrentUserService.UserId;
        var listingProductDetail = await _listingProductDetailRepository.GetListingProductDetailById(id);
        if (listingProductDetail == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: ProductMessages.PRODUCT_NOT_FOUND
            );
        }
        var (relatedProductsList, _) = await _listingProductDetailRepository.GetListListingProductDetail(currentUserId, categoryId: listingProductDetail.CategoryId.ToString());
        var listingProductDetailMapper = _mapper.Map<GetListingProductDetailDto>(listingProductDetail);
        var relatedProductsListMapper = _mapper.Map<List<GetListingProductDetailDto>>(relatedProductsList);
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: ProductMessages.GET_PRODUCT_SUCCESSFULLY,
            data: new
            {
                product = listingProductDetailMapper,
                related_product = relatedProductsListMapper
            }
        );

    }


}

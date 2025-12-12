using System.Net;
using AutoMapper;
using ebay.application.Features.Products;
using MediatR;

public class GetListListingProductDetailQueryHandler : IRequestHandler<GetListListingProductDetailQuery, ResponseService<ResponsePagedService<object>>>
{
    private readonly IMapper _mapper;
    private readonly IListingProductDetailRepository _listingRepo;
    private readonly ICategoryRepository _categoryRepo;

    public GetListListingProductDetailQueryHandler(
        IMapper mapper,
        IListingProductDetailRepository listingRepo,
        ICategoryRepository categoryRepo)
    {
        _mapper = mapper;
        _listingRepo = listingRepo;
        _categoryRepo = categoryRepo;
    }

    public async Task<ResponseService<ResponsePagedService<object>>> Handle(
        GetListListingProductDetailQuery request, CancellationToken cancellationToken)
    {
        var q = request.ListingProductDetailQueryDto;
        string? categoryName = null;

        // PAGE & PAGESIZE
        int page = int.Parse(request.PaginationDto.Page!);
        int pageSize = int.Parse(request.PaginationDto.PageSize!);

        // CATEGORY
        if (!string.IsNullOrWhiteSpace(q.CategoryId))
        {
            if (!int.TryParse(q.CategoryId, out var cateId))
            {
                return ResponseError(CategoryMessages.CATEGORY_ID_MUST_BE_A_NUMBER);
            }
            var category = await _categoryRepo.GetCategoryById(cateId);
            if (category == null)
            {
                return ResponseError(CategoryMessages.CATEGORY_NOT_FOUND);
            }
            else
            {
                categoryName = category.Name;
            }
        }

        // PRICE MIN
        if (q.PriceMin != null && !decimal.TryParse(q.PriceMin, out _))
        {
            return ResponseError(ProductMessages.PRICE_MIN_MUST_BE_A_VALID_NUMBER);

        }

        // PRICE MAX
        if (q.PriceMax != null && !decimal.TryParse(q.PriceMax, out _))
        {
            return ResponseError(ProductMessages.PRICE_MAX_MUST_BE_A_VALID_NUMBER);

        }

        // PRICE RANGE CHECK
        if (decimal.TryParse(q.PriceMin, out var min) &&
            decimal.TryParse(q.PriceMax, out var max))
        {
            if (max < min)
            {
                return ResponseError(ProductMessages.PRICE_MAX_CANNOT_BE_LESS_THAN_PRICE_MIN);
            }
        }

        // RATING FILTER
        if (q.RatingFilter != null)
        {
            if (!int.TryParse(q.RatingFilter, out var rating))
            {
                return ResponseError(ProductMessages.RATING_FILTER_MUST_BE_A_NUMBER);
            }

            if (rating < 1 || rating > 5)
            {
                return ResponseError(ProductMessages.RATING_FILTER_MUST_BE_BETWEEN_1_AND_5);
            }
        }

        // ORDER
        if (q.Order != null)
        {
            var order = q.Order.ToLower();
            if (order != "asc" && order != "desc")
            {
                return ResponseError(ProductMessages.ORDER_MUST_BE_ASC_OR_DESC);
            }
        }

        // SORT BY
        if (q.SortBy != null)
        {
            var allowSort = new[] { "price", "sold", "created_at" };
            if (!allowSort.Contains(q.SortBy.ToLower()))
            {
                return ResponseError(ProductMessages.SORT_BY_MUST_BE_PRICE_SOLD_CREATED_AT);
            }
        }


        var (list, totalRecords) = await _listingRepo.GetListListingProductDetail(
            page,
            pageSize,
            q.Name,
            q.CategoryId,
            q.PriceMin,
            q.PriceMax,
            q.RatingFilter,
            q.Order,
            q.SortBy
        );

        int totalPage = (int)Math.Ceiling((double)totalRecords / pageSize);

        var listMapped = _mapper.Map<List<GetListingProductDetailDto>>(list);

        var data = new ResponsePagedService<object>(
            data: new
            {
                category = new
                {
                    name = categoryName ?? "All categories"
                },
                products = listMapped
            },
            pagination: new Pagination(page, pageSize, totalRecords, totalPage)
        );

        return new ResponseService<ResponsePagedService<object>>(
            statusCode: (int)HttpStatusCode.OK,
            message: ProductMessages.GET_PRODUCTS_SUCCESSFULLY,
            data: data
        );
    }

    private static ResponseService<ResponsePagedService<object>> ResponseError(string msg)
    {
        return new ResponseService<ResponsePagedService<object>>(
            statusCode: (int)HttpStatusCode.BadRequest,
            message: msg
        );
    }
}

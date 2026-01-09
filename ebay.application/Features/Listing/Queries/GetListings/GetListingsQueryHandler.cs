using System.Net;
using System.Text.Json;
using AutoMapper;
using ebay.application.Features.Listing;
using ebay.application.Interfaces;
using ebay.domain.Interfaces;
using MediatR;

public class GetListingsQueryHandler : IRequestHandler<GetListingsQuery, ResponseService<ResponsePagedService<object>>>
{
    private readonly IUserRepository _userRepo;
    private readonly IListingProductDetailRepository _listingProductDetailRepo;

    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;


    public GetListingsQueryHandler(IMapper mapper, IUserRepository userRepo, ICurrentUserService currentUser, IListingProductDetailRepository listingProductDetailRepo)
    {
        _mapper = mapper;
        _userRepo = userRepo;
        _currentUser = currentUser;
        _listingProductDetailRepo = listingProductDetailRepo;
    }
    public async Task<ResponseService<ResponsePagedService<object>>> Handle(GetListingsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var q = request.Dto;

        var currentUser = await _userRepo.FindUserById(userId);
        if (currentUser == null)
        {
            return new ResponseService<ResponsePagedService<object>>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.USER_NOT_FOUND
            );
        }
        var roles = currentUser.UserRoles.Select(r => r.RoleName!.ToLower()).ToList();
        bool isAdmin = roles.Contains(UserRoleEnum.Admin.ToString().ToLower());
        bool isSeller = roles.Contains(UserRoleEnum.Seller.ToString().ToLower());
        string effectiveRole = isAdmin ? UserRoleEnum.Admin.ToString() : UserRoleEnum.Seller.ToString();

        var status = string.IsNullOrEmpty(request.Dto.Status) ? ListingStatusEnum.WaitingApproval.ToString() : request.Dto.Status;

        if (!EnumValidationStringService.IsValidName<ListingStatusEnum>(status))
        {
            return new ResponseService<ResponsePagedService<object>>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: $@"Listing status is invalid, Listing status must be: {EnumValidationStringService.GetValidEnumNames<ListingStatusEnum>()}"
            );
        }

        // PAGE & PAGESIZE
        int page = int.Parse(request.PaginationDto.Page!);
        int pageSize = int.Parse(request.PaginationDto.PageSize!);

        if (q.Order != null)
        {
            var order = q.Order.ToLower();
            if (order != "asc" && order != "desc")
            {
                return new ResponseService<ResponsePagedService<object>>(
                    statusCode: (int)HttpStatusCode.BadRequest,
                    message: ProductMessages.ORDER_MUST_BE_ASC_OR_DESC
                );
            }
        }

        var (list, totalRecords) = await _listingProductDetailRepo.GetListListingPost(userId, effectiveRole, page, pageSize, q.Name, q.Order, status);

        int totalPage = (int)Math.Ceiling((double)totalRecords / pageSize);

        var listMapped = _mapper.Map<List<GetListingProductDetailDto>>(list);

        var data = new ResponsePagedService<object>(
            data: new
            {
                listings = listMapped
            },
            pagination: new Pagination(page, pageSize, totalRecords, totalPage)
        );

        return new ResponseService<ResponsePagedService<object>>(
            statusCode: (int)HttpStatusCode.OK,
            message: ProductMessages.GET_PRODUCTS_SUCCESSFULLY,
            data: data
        );
    }
}
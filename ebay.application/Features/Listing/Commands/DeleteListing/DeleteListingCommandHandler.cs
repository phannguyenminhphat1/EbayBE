using System.Net;
using ebay.application.Features.Listing;
using ebay.domain.Interfaces;
using MediatR;

public class DeleteListingCommandHandler : IRequestHandler<DeleteListingCommand, ResponseService<object>>
{
    private readonly IUserRepository _userRepo;
    private readonly IListingRepository _listingRepo;
    private readonly IProductRepository _productRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;
    public DeleteListingCommandHandler(IListingRepository listingRepo, IProductRepository productRepo, IUnitOfWork unitOfWork, IUserRepository userRepo, ICurrentUserService currentUser)
    {
        _listingRepo = listingRepo;
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _productRepo = productRepo;
    }
    public async Task<ResponseService<object>> Handle(DeleteListingCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var currentUser = await _userRepo.FindUserById(userId);
        if (currentUser == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.USER_NOT_FOUND
            );
        }
        var roles = currentUser.UserRoles.Select(r => r.RoleName!.ToLower()).ToList();
        bool isAdmin = roles.Contains(UserRoleEnum.Admin.ToString().ToLower());
        bool isSeller = roles.Contains(UserRoleEnum.Seller.ToString().ToLower());
        string effectiveRole = isAdmin ? UserRoleEnum.Admin.ToString() : UserRoleEnum.Seller.ToString();
        if (request.Dto.Ids!.Count == 0)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: ListingMessages.LISTING_IDS_ARE_REQUIRED
            );
        }
        var lstListing = await _listingRepo.GetByIds(request.Dto.Ids, userId, effectiveRole);
        if (lstListing is not null && lstListing.Count != request.Dto.Ids.Count)
        {
            var foundIds = lstListing.Select(x => x.Id).ToHashSet();
            var missingIds = request.Dto.Ids.Where(id => !foundIds.Contains(id)).ToList();

            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: @$"{ListingMessages.LISTING_NOT_FOUND} - Missing listing ids: {missingIds}"
            );
        }
        var listingProductIds = lstListing!.Select(x => x.ProductId!.Value).ToList();

        await _listingRepo.SoftDeleteByIds(request.Dto.Ids, userId, effectiveRole);
        await _productRepo.SoftDeleteByListingIds(listingProductIds);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: ListingMessages.DELETE_LISTING_SUCCESSFULLY
        );


    }
}
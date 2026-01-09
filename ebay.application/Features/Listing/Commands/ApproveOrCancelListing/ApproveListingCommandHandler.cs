using System.Net;
using ebay.application.Features.Listing;
using ebay.domain.Interfaces;
using MediatR;

public class ApproveOrCancelListingCommandHandler : IRequestHandler<ApproveOrCancelListingCommand, ResponseService<string>>
{
    private readonly IUserRepository _userRepo;
    private readonly IListingRepository _listingRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;
    public ApproveOrCancelListingCommandHandler(IListingRepository listingRepo, IUnitOfWork unitOfWork, IUserRepository userRepo, ICurrentUserService currentUser)
    {
        _listingRepo = listingRepo;
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }
    public async Task<ResponseService<string>> Handle(ApproveOrCancelListingCommand request, CancellationToken cancellationToken)
    {
        var listing = await _listingRepo.GetById(request.Dto.Id!.Value);
        if (listing == null)
        {
            return new ResponseService<string>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: ListingMessages.LISTING_NOT_FOUND
            );
        }
        if (listing.Status != ListingStatusEnum.WaitingApproval.ToString())
        {
            return new ResponseService<string>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: ListingMessages.LISTING_STATUS_MUST_BE_WAITING
            );
        }
        if (request.Dto.Status != ListingStatusEnum.Canceled.ToString() && request.Dto.Status != ListingStatusEnum.Active.ToString())
        {
            return new ResponseService<string>(
                statusCode: (int)HttpStatusCode.BadRequest,
                message: $@"Listing status is invalid, Listing status must be: Canceled or Active"
            );
        }

        listing.UpdateStatus(request.Dto.Status!);
        await _listingRepo.Update(listing);
        var msg = request.Dto.Status == ListingStatusEnum.Canceled.ToString() ? "Canceled listing successfully" : "Approve listing successfully";
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<string>(
            statusCode: (int)HttpStatusCode.OK,
            message: msg
        );
    }
}
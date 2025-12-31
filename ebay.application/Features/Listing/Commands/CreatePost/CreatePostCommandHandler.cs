using System.Net;
using System.Text.Json;
using ebay.application.Features.Listing.Commands;
using ebay.domain.Entities;
using ebay.domain.Interfaces;
using MediatR;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ResponseService<object>>
{
    private readonly IListingRepository _listingRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepo;
    private readonly ICurrentUserService _currentUser;

    private readonly IUserRepository _userRepo;


    public CreatePostCommandHandler(IListingRepository listingRepo, IUserRepository userRepo, IProductRepository productRepo, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _listingRepo = listingRepo;
        _productRepo = productRepo;
        _userRepo = userRepo;

    }
    public async Task<ResponseService<object>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var sellerId = _currentUser.UserId;
        var currentUser = await _userRepo.FindUserById(sellerId);
        if (currentUser == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.USER_NOT_FOUND
            );
        }
        var dto = request.Dto;
        var product = new ProductEntity(dto.ProductName, dto.ProductDescription, dto.StartingPrice, dto.Stock);
        foreach (var item in dto.Images!)
        {
            product.AddImage(item, false);
        }
        await _productRepo.Add(product);
        var listing = new ListingEntity(sellerId, dto.CategoryId, dto.Title!, dto.ListingDescription, dto.StartingPrice, false, DateTime.Now.AddDays(7), product.Id, ListingStatusEnum.WaitingApproval.ToString());
        listing.AttachProduct(product);
        await _listingRepo.Add(listing);
        await _userRepo.AddRoleToUser(currentUser.Id, (int)UserRoleEnum.Seller);
        await _unitOfWork.SaveChangesAsync();

        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: ListingMessages.CREATE_POST_SUCCESSFULLY
        );
    }
}
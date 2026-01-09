using MediatR;

namespace ebay.application.Features.Listing;

public record ApproveOrCancelListingCommand(ListingStatusDto Dto) : IRequest<ResponseService<string>>;
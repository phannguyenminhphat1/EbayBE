using MediatR;

namespace ebay.application.Features.Listing;

public record ApproveOrCancelListingCommand(string Id, ListingStatusDto Dto) : IRequest<ResponseService<string>>;
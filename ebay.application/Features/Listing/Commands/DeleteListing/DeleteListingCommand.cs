using MediatR;

namespace ebay.application.Features.Listing;

public record DeleteListingCommand(DeleteListingDto Dto) : IRequest<ResponseService<object>>;
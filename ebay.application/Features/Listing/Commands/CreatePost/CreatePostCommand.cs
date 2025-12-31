using MediatR;

namespace ebay.application.Features.Listing.Commands;

public record CreatePostCommand(CreatePostDto Dto) : IRequest<ResponseService<object>>;
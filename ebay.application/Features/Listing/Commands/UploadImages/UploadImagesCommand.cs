using MediatR;

namespace ebay.application.Features.Listing;

public record UploadImagesCommand(UploadImagesDto Dto) : IRequest<ResponseService<List<string>>>;
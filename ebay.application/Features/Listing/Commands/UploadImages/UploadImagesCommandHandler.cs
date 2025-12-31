using System.Net;
using ebay.application.Features.Listing;
using MediatR;

public class UploadImagesCommandHandler : IRequestHandler<UploadImagesCommand, ResponseService<List<string>>>
{
    public async Task<ResponseService<List<string>>> Handle(UploadImagesCommand request, CancellationToken cancellationToken)
    {

        if (!UploadMultipleHandler.UploadMultipleFiles<List<string>>(request.Dto.Images!, out List<string>? urls, out var errorResponse))
        {
            return errorResponse!;
        }

        return new ResponseService<List<string>>(
            statusCode: (int)HttpStatusCode.OK,
            message: ListingMessages.UPLOAD_IMAGES_SUCCESSFULLY,
            data: urls
        );
    }
}
using System.Net;
using AutoMapper;
using ebay.application.Features.Users;
using ebay.domain.Interfaces;
using MediatR;

public class UploadAvatarCommandHandler : IRequestHandler<UploadAvatarCommand, ResponseService<string>>
{
    public async Task<ResponseService<string>> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
    {

        if (!UploadHandler.UploadFile<string>(request.Dto.Ava!, out string? urlString, out var errorResponse))
        {
            return errorResponse!;
        }

        return new ResponseService<string>(
            statusCode: (int)HttpStatusCode.OK,
            message: UserMessages.UPLOAD_FILE_SUCCESSFULLY,
            data: urlString
        );
    }
}
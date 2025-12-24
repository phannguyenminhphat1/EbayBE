using MediatR;

namespace ebay.application.Features.Users;

public record UploadAvatarCommand(UploadAvatarDto Dto) : IRequest<ResponseService<string>>;
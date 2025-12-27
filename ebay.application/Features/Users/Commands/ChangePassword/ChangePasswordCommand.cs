using MediatR;

namespace ebay.application.Features.Users.Commands;

public record ChangePasswordCommand(ChangePasswordDto Dto) : IRequest<ResponseService<object>>;
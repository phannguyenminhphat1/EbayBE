using MediatR;

namespace ebay.application.Features.Users.Commands;

public record ChangePasswordCommand(int UserId, ChangePasswordDto Dto) : IRequest<ResponseService<object>>;
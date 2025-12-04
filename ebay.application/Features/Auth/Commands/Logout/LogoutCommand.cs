using MediatR;
namespace ebay.application.Features.Auth.Commands.Logout;

public record LogoutCommand(RefreshTokenDto Dto) : IRequest<ResponseService<string>>;

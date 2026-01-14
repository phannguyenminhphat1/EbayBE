using MediatR;

namespace ebay.application.Features.Auth;

public record LoginGoogleOAuthCommand(string Code) : IRequest<ResponseService<TokenResponse<UserDto>>>;
using MediatR;
namespace ebay.application.Features.Auth.Commands.Login;

public record LoginCommand(LoginDto LoginDto) : IRequest<ResponseService<TokenResponse>>;

using ebay.domain.Entities;
using MediatR;
namespace ebay.application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(RefreshTokenDto Dto) : IRequest<ResponseService<TokenResponse<UserDto>>>;

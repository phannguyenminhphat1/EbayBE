using System.Net;
using System.Text.Json;
using ebay.domain.Entities;
using ebay.domain.Interfaces;
using MediatR;

namespace ebay.application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseService<TokenResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _refreshTokenRepository = refreshTokenRepository;
    }
    public async Task<ResponseService<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindUserByEmail(request.LoginDto.Email);
        if (user == null)
        {
            return new ResponseService<TokenResponse>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.EMAIL_OR_PASSWORD_IS_INCORRECT
            );
        }
        var checkPassword = _userRepository.ValidateHashPassword(request.LoginDto.PasswordHash, user.PasswordHash);
        if (!checkPassword)
        {
            return new ResponseService<TokenResponse>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.EMAIL_OR_PASSWORD_IS_INCORRECT
            );
        }
        var accessToken = _jwtService.GenerateToken(user);
        var generateRefreshToken = _jwtService.GenerateRefreshToken();
        var refreshToken = new RefreshTokenEntity(generateRefreshToken, user.Id, DateTime.Now, DateTime.Now.AddDays(7));
        await _refreshTokenRepository.AddRefreshToken(refreshToken);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<TokenResponse>(
            statusCode: (int)HttpStatusCode.OK,
            message: UserMessages.LOGIN_SUCCESSFULLY,
            data: new TokenResponse(accessToken, generateRefreshToken)
        );
    }
}

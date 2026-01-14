using System.Net;
using System.Text.Json;
using AutoMapper;
using ebay.application.Features.Auth;
using ebay.domain.Entities;
using ebay.domain.Interfaces;
using MediatR;

public class LoginGoogleOAuthCommandHandler : IRequestHandler<LoginGoogleOAuthCommand, ResponseService<TokenResponse<UserDto>>>
{
    private readonly IGoogleOAuthService _googleOAuthService;
    private readonly IUserRepository _userRepo;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public LoginGoogleOAuthCommandHandler(IGoogleOAuthService googleOAuthService, IUserRepository userRepo, IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper, IRefreshTokenRepository refreshTokenRepository)
    {
        _googleOAuthService = googleOAuthService;
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }
    public async Task<ResponseService<TokenResponse<UserDto>>> Handle(LoginGoogleOAuthCommand request, CancellationToken cancellationToken)
    {
        var token = await _googleOAuthService.ExchangeCodeAsync(request.Code);
        var googleUser = await _googleOAuthService.VerifyIdToken(token.IdToken);
        var user = await _userRepo.FindUserByEmail(googleUser.Email!);
        if (user == null)
        {
            var hashPassword = _userRepo.HashPassword(123456789.ToString());
            var username = googleUser.Email!.Split("@")[0];
            user = new UserEntity(username, hashPassword, googleUser.Name!, googleUser.Email, DateTime.Now, false, googleUser.Picture);
            user.AddRole((int)UserRoleEnum.Buyer);
            await _userRepo.AddUser(user);
            await _unitOfWork.SaveChangesAsync();
        }
        var userAgain = await _userRepo.FindUserByEmail(user.Email);
        var accessToken = _jwtService.GenerateToken(userAgain!);
        var generateRefreshToken = _jwtService.GenerateRefreshToken();
        var refreshToken = new RefreshTokenEntity(generateRefreshToken, userAgain!.Id, DateTime.Now, DateTime.Now.AddDays(7));
        var userMapper = _mapper.Map<UserDto>(userAgain);
        await _refreshTokenRepository.AddRefreshToken(refreshToken);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<TokenResponse<UserDto>>(
            statusCode: (int)HttpStatusCode.OK,
            message: UserMessages.LOGIN_SUCCESSFULLY,
            data: new TokenResponse<UserDto>(accessToken, generateRefreshToken, userMapper)
        );

    }
}
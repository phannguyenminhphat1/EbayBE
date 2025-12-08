using System.Net;
using AutoMapper;
using ebay.application.Features.Auth.Commands.RefreshToken;
using ebay.domain.Entities;
using ebay.domain.Interfaces;
using MediatR;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ResponseService<TokenResponse<UserDto>>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;



    public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IJwtService jwtService, IUnitOfWork unitOfWork, ICurrentUserService currentUser, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _jwtService = jwtService;
        _userRepository = userRepository;
        _mapper = mapper;

    }
    public async Task<ResponseService<TokenResponse<UserDto>>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var rfToken = await _refreshTokenRepository.GetByTokenAndUserId(request.Dto.RefreshToken!, userId);
        if (rfToken == null)
        {
            return new ResponseService<TokenResponse<UserDto>>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.REFRESH_TOKEN_NOT_FOUND_OR_ALREADY_USED
            );
        }
        if (rfToken.ExpiresAt < DateTime.Now)
        {
            return new ResponseService<TokenResponse<UserDto>>(
                statusCode: (int)HttpStatusCode.Unauthorized,
                message: UserMessages.REFRESH_TOKEN_IS_EXPIRED
            );
        }
        var user = await _userRepository.FindUserById(userId);
        await _refreshTokenRepository.DeleteTokenById(rfToken.Id);

        var newAccessToken = _jwtService.GenerateToken(user!);
        var newRefreshToken = _jwtService.GenerateRefreshToken();
        var refreshToken = new RefreshTokenEntity(newRefreshToken, user!.Id, DateTime.Now, rfToken.ExpiresAt!.Value);
        var userMapper = _mapper.Map<UserDto>(user);

        await _refreshTokenRepository.AddRefreshToken(refreshToken);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<TokenResponse<UserDto>>(
            statusCode: (int)HttpStatusCode.OK,
            message: UserMessages.REFRESH_TOKEN_SUCCESSFULLY,
            data: new TokenResponse<UserDto>(newAccessToken, newRefreshToken, userMapper)
        );

        throw new NotImplementedException();
    }
}
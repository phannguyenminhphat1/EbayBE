using System.Net;
using ebay.application.Features.Auth.Commands.Logout;
using ebay.domain.Interfaces;
using MediatR;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ResponseService<string>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUser;


    public LogoutCommandHandler(IUnitOfWork unitOfWork, IRefreshTokenRepository refreshTokenRepository, ICurrentUserService currentUser)
    {
        _unitOfWork = unitOfWork;
        _refreshTokenRepository = refreshTokenRepository;
        _currentUser = currentUser;

    }
    public async Task<ResponseService<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var rfToken = await _refreshTokenRepository.GetByTokenAndUserId(request.Dto.RefreshToken!, userId);
        if (rfToken == null)
        {
            return new ResponseService<string>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.REFRESH_TOKEN_NOT_FOUND_OR_ALREADY_USED
            );
        }
        await _refreshTokenRepository.DeleteTokenById(rfToken.Id);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<string>(
            statusCode: (int)HttpStatusCode.OK,
            message: UserMessages.LOGOUT_SUCCESSFULLY
        );
    }
}
using System.Net;
using AutoMapper;
using ebay.application.Features.Users.Commands;
using ebay.domain.Interfaces;
using MediatR;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ResponseService<object>>
{
    private readonly IUserRepository _userRepo;
    private readonly IUnitOfWork _unitOfWork;

    private readonly ICurrentUserService _currentUser;

    public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepo, ICurrentUserService currentUser)
    {
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }
    public async Task<ResponseService<object>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var user = await _userRepo.FindUserById(userId);
        if (user == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.USER_NOT_FOUND
            );
        }
        var checkOldPassword = _userRepo.ValidateHashPassword(request.Dto.OldPassword!, user.PasswordHash);
        if (!checkOldPassword)
        {
            return new ResponseService<object>(
               statusCode: (int)HttpStatusCode.UnprocessableEntity,
               message: CommonMessages.ERROR,
               data: new
               {
                   old_password = UserMessages.OLD_PASSWORD_IS_INCORRECT
               }
            );
        }

        if (request.Dto.ConfirmPassword != request.Dto.NewPassword)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.UnprocessableEntity,
                message: CommonMessages.ERROR,
                data: new
                {
                    confirm_password = UserMessages.CONFIRM_PASSWORD_DOES_NOT_MATCH_PASSWORD
                }
            );
        }
        var hashPassword = _userRepo.HashPassword(request.Dto.NewPassword!);
        user.UpdateProfile(null, null, null, null, null, hashPassword);
        await _userRepo.UpdateMe(user);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: UserMessages.CHANGE_PASSWORD_SUCCESSFULLY
        );

    }
}
using System.Net;
using System.Text.Json;
using ebay.application.Features.Auth.Commands.Register;
using ebay.domain.Entities;
using ebay.domain.Interfaces;
using MediatR;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseService<object>>
{
    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseService<object>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FindUserByEmail(request.Dto.Email);
        if (existingUser != null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.UnprocessableEntity,
                message: CommonMessages.ERROR,
                data: new
                {
                    email = UserMessages.EMAIL_IS_ALREADY_EXIST
                }
            );
        }
        if (request.Dto.ConfirmPassword != request.Dto.Password)
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
        var hashPassword = _userRepository.HashPassword(request.Dto.Password);
        var userEntity = new UserEntity(request.Dto.Username, hashPassword, request.Dto.FullName, request.Dto.Email, DateTime.Now, false);
        userEntity.AddRole((int)UserRoleEnum.Buyer);
        await _userRepository.AddUser(userEntity);
        await _unitOfWork.SaveChangesAsync();
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: UserMessages.REGISTER_SUCCESSFULLY
        );
    }
}


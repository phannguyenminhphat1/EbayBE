using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using ebay.application.Features.Users.Commands;
using ebay.domain.Interfaces;
using MediatR;

public class UpdateMeCommandHandler : IRequestHandler<UpdateMeCommand, ResponseService<object>>
{
    private readonly IUserRepository _userRepo;

    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    private readonly ICurrentUserService _currentUser;

    public UpdateMeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepo, ICurrentUserService currentUser)
    {
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _mapper = mapper;
    }
    public async Task<ResponseService<object>> Handle(UpdateMeCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var errors = new Dictionary<string, string>();

        ValidateOptional(request.Dto.Username, "username", errors);
        ValidateOptional(request.Dto.FullName, "fullname", errors);
        ValidateOptional(request.Dto.Ava, "ava", errors);
        ValidateOptional(request.Dto.Phone, "phone", errors);
        ValidateOptional(request.Dto.Address, "address", errors);

        if (errors.Count > 0)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.UnprocessableEntity,
                message: CommonMessages.ERROR,
                data: errors
            );
        }
        var user = await _userRepo.FindUserById(userId);
        if (user == null)
        {
            return new ResponseService<object>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.USER_NOT_FOUND
            );
        }
        user.UpdateProfile(request.Dto.Username, request.Dto.FullName, request.Dto.Phone, request.Dto.Address, request.Dto.Ava, null);
        await _userRepo.UpdateMe(user);
        await _unitOfWork.SaveChangesAsync();
        var userMapper = _mapper.Map<UserDto>(user);
        return new ResponseService<object>(
            statusCode: (int)HttpStatusCode.OK,
            message: UserMessages.UPDATE_PROFILE_SUCCESSFULLY,
            data: userMapper
        );
    }

    private static Dictionary<string, string> ValidateOptional(string? value, string fieldName, Dictionary<string, string> errors)
    {
        if (value != null && string.IsNullOrWhiteSpace(value)) errors[fieldName.ToLower()] = "must not be empty";
        return errors;
    }


}

using System.Net;
using AutoMapper;
using ebay.application.Features.Users;
using ebay.domain.Interfaces;
using MediatR;


public class GetMeQueryHandler : IRequestHandler<GetMeQuery, ResponseService<GetMeDto>>
{
    private readonly IUserRepository _userRepo;

    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    private readonly ICurrentUserService _currentUser;

    public GetMeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepo, ICurrentUserService currentUser)
    {
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _mapper = mapper;
    }
    public async Task<ResponseService<GetMeDto>> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var user = await _userRepo.FindUserById(userId);
        if (user == null)
        {
            return new ResponseService<GetMeDto>(
                statusCode: (int)HttpStatusCode.NotFound,
                message: UserMessages.USER_NOT_FOUND
            );
        }
        var userMapper = _mapper.Map<GetMeDto>(user);
        return new ResponseService<GetMeDto>(
            statusCode: (int)HttpStatusCode.OK,
            message: UserMessages.GET_USER_SUCCESSFULLY,
            data: userMapper
        );
    }
}
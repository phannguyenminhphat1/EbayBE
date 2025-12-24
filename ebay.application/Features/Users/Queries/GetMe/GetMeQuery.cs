using MediatR;

namespace ebay.application.Features.Users;

public record GetMeQuery(int UserId) : IRequest<ResponseService<GetMeDto>>;
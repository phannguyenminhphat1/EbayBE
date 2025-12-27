using MediatR;

namespace ebay.application.Features.Users;

public record GetMeQuery() : IRequest<ResponseService<GetMeDto>>;
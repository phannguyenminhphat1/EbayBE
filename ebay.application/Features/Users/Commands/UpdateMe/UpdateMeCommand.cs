using MediatR;

namespace ebay.application.Features.Users.Commands;

public record UpdateMeCommand(int UserId, UpdateMeDto Dto) : IRequest<ResponseService<object>>;
using MediatR;

namespace ebay.application.Features.Users.Commands;

public record UpdateMeCommand(UpdateMeDto Dto) : IRequest<ResponseService<object>>;
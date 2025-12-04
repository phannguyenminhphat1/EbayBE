using MediatR;

namespace ebay.application.Features.Auth.Commands.Register;

public record RegisterCommand(RegisterDto Dto) : IRequest<ResponseService<object>>;

using MediatR;

namespace ebay.application.Features.Orders.Commands;

public record RejectAndConfirmOrderCommand(string Id, RejectAndConfirmDto Dto) : IRequest<ResponseService<object>>;
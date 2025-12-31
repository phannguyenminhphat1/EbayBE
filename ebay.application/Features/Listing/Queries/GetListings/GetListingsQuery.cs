using MediatR;

namespace ebay.application.Features.Listing;

public record GetListingsQuery(GetListingsQueryDto Dto, PaginationDto PaginationDto) : IRequest<ResponseService<ResponsePagedService<object>>>;
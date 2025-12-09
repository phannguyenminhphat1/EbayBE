using MediatR;

public record GetCategoriesQuery : IRequest<ResponseService<List<CategoryDto>>>;
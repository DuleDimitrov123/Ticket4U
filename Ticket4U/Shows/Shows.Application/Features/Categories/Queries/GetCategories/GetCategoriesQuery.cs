using MediatR;

namespace Shows.Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesQuery : IRequest<IList<CategoryResponse>>
{
}

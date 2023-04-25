using MediatR;

namespace Shows.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<CategoryResponse>
{
    public Guid CategoryId { get; set; }
}

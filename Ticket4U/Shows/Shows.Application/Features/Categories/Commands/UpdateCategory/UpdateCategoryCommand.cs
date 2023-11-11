using MediatR;

namespace Shows.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Unit>
{
    public Guid CategoryId { get; set; }

    public string NewName { get; set; }

    public string NewDescription { get; set; }
}

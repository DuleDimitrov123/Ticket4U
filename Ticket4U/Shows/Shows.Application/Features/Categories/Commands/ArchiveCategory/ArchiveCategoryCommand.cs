using MediatR;

namespace Shows.Application.Features.Categories.Commands.ArchiveCategory;

public class ArchiveCategoryCommand : IRequest<Unit>
{
    public Guid CategoryId { get; set; }
}

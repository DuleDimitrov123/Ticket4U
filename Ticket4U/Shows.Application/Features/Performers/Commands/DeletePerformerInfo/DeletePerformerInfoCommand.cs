using MediatR;

namespace Shows.Application.Features.Performers.Commands.DeletePerformerInfo;

public class DeletePerformerInfoCommand : IRequest<Unit>
{
    public Guid PerformerId { get; set; }

    public IList<string> PerformerInfoNamesToDelete { get; set; }
}

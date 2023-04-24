using MediatR;
using Shows.Domain.Performers;

namespace Shows.Application.Performers.Commands.CreatePerformerInfo;

public class UpdatePerformerInfoCommand : IRequest<Unit>
{
    public Guid PerformerId { get; set; }

    public IList<PerformerInfo> PerformerInfos { get; set; }
}

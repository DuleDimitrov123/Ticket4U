using MediatR;
using Shows.Domain.Performers;

namespace Shows.Application.Performers.Commands.CreatePerformerInfo;

public class UpdatePerformerInfoCommand : IRequest<Unit>
{
    public Guid PerformerId { get; set; }

    public Dictionary<string, string> PerformerInfos { get; set; }
}

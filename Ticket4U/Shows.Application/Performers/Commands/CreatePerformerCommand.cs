using MediatR;
using Shows.Domain.Performers;

namespace Shows.Application.Performers.Commands;

public class CreatePerformerCommand : IRequest<Guid>
{
    public string Name { get; set; }

    public IList<PerformerInfo> PerformerInfos { get; set; }
}

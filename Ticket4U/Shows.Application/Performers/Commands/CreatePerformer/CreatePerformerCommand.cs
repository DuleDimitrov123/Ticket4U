using MediatR;
using Shows.Domain.Performers;

namespace Shows.Application.Performers.Commands.CreatePerformer;

public class CreatePerformerCommand : IRequest<Guid>
{
    public string Name { get; set; }

    public Dictionary<string, string> PerformerInfos { get; set; }
}

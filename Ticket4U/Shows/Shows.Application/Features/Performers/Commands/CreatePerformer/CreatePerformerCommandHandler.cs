using MediatR;
using Shared.Application.Contracts.Persistence;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Commands.CreatePerformer;

public class CreatePerformerCommandHandler : IRequestHandler<CreatePerformerCommand, Guid>
{
    private readonly ICommandRepository<Performer> _commandRepository;

    public CreatePerformerCommandHandler(ICommandRepository<Performer> commandRepository)
    {
        _commandRepository = commandRepository;
    }

    public async Task<Guid> Handle(CreatePerformerCommand request, CancellationToken cancellationToken)
    {
        var newPerformer = Performer.Create(request.Name);

        if (request.PerformerInfos != null)
        {
            foreach (KeyValuePair<string, string> entry in request.PerformerInfos)
            {
                newPerformer.AddPerformerInfo(PerformerInfo.Create(entry.Key, entry.Value));
            }
        }

        newPerformer = await _commandRepository.Add(newPerformer);

        return newPerformer.Id;
    }
}
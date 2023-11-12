using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Commands.CreatePerformerInfo;

public class UpdatePerformerInfoCommandHandler : IRequestHandler<UpdatePerformerInfoCommand, Unit>
{
    private readonly IPerformerQueryRepository _queryRepository;
    private readonly ICommandRepository<Performer> _commandRepository;

    public UpdatePerformerInfoCommandHandler(IPerformerQueryRepository queryRepository, ICommandRepository<Performer> commandRepository)
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
    }

    public async Task<Unit> Handle(UpdatePerformerInfoCommand request, CancellationToken cancellationToken)
    {
        var performer = await _queryRepository.GetPerformerWithPerformerInfos(request.PerformerId);

        if (performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.PerformerId);
        }

        foreach (KeyValuePair<string, string> entry in request.PerformerInfos)
        {
            performer.AddPerformerInfo(PerformerInfo.Create(entry.Key, entry.Value));
        }

        await _commandRepository.Update(performer);

        return Unit.Value;
    }
}

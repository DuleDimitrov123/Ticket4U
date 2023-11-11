using MediatR;
using Shows.Application.Contracts.Persistance;
using Shared.Application.Exceptions;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Commands.CreatePerformerInfo;

public class UpdatePerformerInfoCommandHandler : IRequestHandler<UpdatePerformerInfoCommand, Unit>
{
    private readonly IPerformerRepository _repository;

    public UpdatePerformerInfoCommandHandler(IPerformerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdatePerformerInfoCommand request, CancellationToken cancellationToken)
    {
        var performer = await _repository.GetPerformerWithPerformerInfos(request.PerformerId);

        if (performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.PerformerId);
        }

        foreach (KeyValuePair<string, string> entry in request.PerformerInfos)
        {
            performer.AddPerformerInfo(PerformerInfo.Create(entry.Key, entry.Value));
        }

        await _repository.Update(performer);

        return Unit.Value;
    }
}

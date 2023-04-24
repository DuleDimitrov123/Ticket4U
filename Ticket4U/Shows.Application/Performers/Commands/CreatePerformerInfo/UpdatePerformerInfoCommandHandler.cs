using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Performers;

namespace Shows.Application.Performers.Commands.CreatePerformerInfo;

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

        performer.AddPerfomerInfos(request.PerformerInfos);
        await _repository.Update(performer);

        return Unit.Value;
    }
}

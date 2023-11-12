using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Commands.DeletePerformerInfo;

public class DeletePerformerInfoCommandHandler : IRequestHandler<DeletePerformerInfoCommand, Unit>
{
    private readonly IPerformerQueryRepository _queryRepository;
    private readonly ICommandRepository<Performer> _commandRepository;

    public DeletePerformerInfoCommandHandler(IPerformerQueryRepository repository, ICommandRepository<Performer> commandRepository)
    {
        _queryRepository = repository;
        _commandRepository = commandRepository;
    }

    public async Task<Unit> Handle(DeletePerformerInfoCommand request, CancellationToken cancellationToken)
    {
        var performer = await _queryRepository.GetPerformerWithPerformerInfos(request.PerformerId);

        if (performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.PerformerId);
        }

        performer.RemovePerformerInfo(request.PerformerInfoNamesToDelete);
        await _commandRepository.Update(performer);

        return Unit.Value;
    }
}

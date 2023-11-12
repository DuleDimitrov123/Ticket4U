using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Shows;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Shows.Commands.UpdateShowStartingDateTime;

public class UpdateShowStartingDateTimeCommandHandler : IRequestHandler<UpdateShowStartingDateTimeCommand, Unit>
{
    private readonly IShowQueryRepository _showQueryRepository;
    private readonly ICommandRepository<Show> _commandRepository;

    public UpdateShowStartingDateTimeCommandHandler(IShowQueryRepository repository, ICommandRepository<Show> commandRepository)
    {
        _showQueryRepository = repository;
        _commandRepository = commandRepository;
    }

    public async Task<Unit> Handle(UpdateShowStartingDateTimeCommand request, CancellationToken cancellationToken)
    {
        var show = await _showQueryRepository.GetShowByExternalId(request.ExternalShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ExternalShowId);
        }

        show.UpdateStartingDateTime(request.NewStartingDateTime);
        await _commandRepository.Update(show);

        return Unit.Value;
    }
}

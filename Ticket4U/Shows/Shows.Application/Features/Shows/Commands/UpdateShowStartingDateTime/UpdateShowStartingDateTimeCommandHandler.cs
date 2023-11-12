using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shared.Domain.Events;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Notifications.ShowStartingDateTimeUpdated;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.UpdateShowStartingDateTime;

public class UpdateShowStartingDateTimeCommandHandler : IRequestHandler<UpdateShowStartingDateTimeCommand, Unit>
{
    private readonly IShowQueryRepository _showQueryRepository;
    private readonly ICommandRepository<Show> _commandRepository;
    private readonly IMediator _mediator;

    public UpdateShowStartingDateTimeCommandHandler(IMediator mediator, IShowQueryRepository showQueryRepository, ICommandRepository<Show> commandRepository)
    {
        _mediator = mediator;
        _showQueryRepository = showQueryRepository;
        _commandRepository = commandRepository;
    }

    public async Task<Unit> Handle(UpdateShowStartingDateTimeCommand request, CancellationToken cancellationToken)
    {
        var show = await _showQueryRepository.GetShowWithShowMessages(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        var oldStartingDateTime = show.StartingDateTime;

        show.UpdateStartingDateTime(request.NewStartingDateTime);
        await _commandRepository.Update(show);

        await _mediator.Publish(
            new ShowStartingDateTimeUpdatedNotification(
                new UpdatedShowsStartingDateTimeEvent(show.Id, oldStartingDateTime, show.StartingDateTime)));

        return Unit.Value;
    }
}

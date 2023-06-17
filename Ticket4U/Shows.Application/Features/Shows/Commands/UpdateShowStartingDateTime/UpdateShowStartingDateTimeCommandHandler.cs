using MediatR;
using Shared.Domain.Events;
using Shows.Application.Contracts.Persistance;
using Shared.Application.Exceptions;
using Shows.Application.Features.Shows.Notifications.ShowStartingDateTimeUpdated;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.UpdateShowStartingDateTime;

public class UpdateShowStartingDateTimeCommandHandler : IRequestHandler<UpdateShowStartingDateTimeCommand, Unit>
{
    private readonly IShowRepository _repository;
    private readonly IMediator _mediator;

    public UpdateShowStartingDateTimeCommandHandler(IShowRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(UpdateShowStartingDateTimeCommand request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetShowWithShowMessages(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        var oldStartingDateTime = show.StartingDateTime;

        show.UpdateStartingDateTime(request.NewStartingDateTime);
        await _repository.Update(show);

        await _mediator.Publish(
            new ShowStartingDateTimeUpdatedNotification(
                new UpdatedShowsStartingDateTimeEvent(show.Id, oldStartingDateTime, show.StartingDateTime)));

        return Unit.Value;
    }
}

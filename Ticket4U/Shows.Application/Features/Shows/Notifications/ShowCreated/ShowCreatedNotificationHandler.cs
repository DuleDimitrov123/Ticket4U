using AutoMapper;
using MediatR;
using Shared.Domain.Events;
using Shows.Application.Contracts.Outbox;

namespace Shows.Application.Features.Shows.Notifications.ShowCreated;

public class ShowCreatedNotificationHandler : INotificationHandler<ShowCreatedNotification>
{
    private readonly IMapper _mapper;
    private readonly IShowPublisher _showPublisher;

    public ShowCreatedNotificationHandler(IMapper mapper, IShowPublisher showPublisher)
    {
        _mapper = mapper;
        _showPublisher = showPublisher;
    }

    public async Task Handle(ShowCreatedNotification notification, CancellationToken cancellationToken)
    {
        var createdShowEvent = _mapper.Map<CreatedShowEvent>(notification.Show);

        Console.WriteLine($"Show created! Name: {createdShowEvent.Name}");

        await _showPublisher.PublishCreatedShow(createdShowEvent);
    }
}

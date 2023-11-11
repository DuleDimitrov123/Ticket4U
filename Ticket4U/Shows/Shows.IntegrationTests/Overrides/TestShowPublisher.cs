using Shared.Domain.Events;
using Shows.Application.Contracts.Outbox;

namespace Shows.IntegrationTests.Overrides;

public class TestShowPublisher : IShowPublisher
{
    public Task PublishCreatedShow(CreatedShowEvent createdShowEvent)
    {
        Console.WriteLine($"PUBLISHING CreatedShowEvent with Name: {createdShowEvent.Name}");

        return Task.CompletedTask;
    }

    public Task PublishUpdatedShowDateTime(UpdatedShowsStartingDateTimeEvent updatedShowsStartingDateTimeEvent)
    {
        Console.WriteLine($"PUBLISHING UpdatedShowsStartingDateTimeEvent with ShowId: {updatedShowsStartingDateTimeEvent.ShowId}");

        return Task.CompletedTask;
    }
}

using DotNetCore.CAP;
using Shows.Application.Contracts.Outbox;
using Shared.Domain.Events;
using Shows.Infrastructure.Persistance;
using Shared.Domain.Events.Constants;

namespace Shows.Infrastructure.Outbox;

public class ShowPublisher : IShowPublisher
{
    private readonly ICapPublisher _capPublisher;
    private readonly ShowsDbContext _dbContext;

    public ShowPublisher(ICapPublisher capPublisher, ShowsDbContext dbContext)
    {
        _capPublisher = capPublisher;
        _dbContext = dbContext;
    }

    public async Task PublishCreatedShow(CreatedShowEvent createdShowEvent)
    {
        using(var transaction = _dbContext.Database.BeginTransaction(_capPublisher, true))
        {
            await _capPublisher.PublishAsync(ShowDomainEventsConstants.NewShowCreated, createdShowEvent);
        }
    }

    public async Task PublishUpdatedShowDateTime(UpdatedShowsStartingDateTimeEvent updatedShowsStartingDateTimeEvent)
    {
        using (var transaction = _dbContext.Database.BeginTransaction(_capPublisher, true))
        {
            await _capPublisher.PublishAsync(ShowDomainEventsConstants.ShowStartingDateTimeUpdated, updatedShowsStartingDateTimeEvent);
        }
    }
}

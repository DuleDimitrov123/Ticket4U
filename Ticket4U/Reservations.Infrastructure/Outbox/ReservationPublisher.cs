using DotNetCore.CAP;
using Reservations.Application.Contracts.Outbox;
using Shared.Domain.Events;
using Shared.Domain.Events.Constants;

namespace Reservations.Infrastructure.Outbox;

public class ReservationPublisher : IReservationPublisher
{
    private readonly ICapPublisher _capPublisher;
    private readonly ReservationsDbContext _dbContext;

    public ReservationPublisher(ICapPublisher capPublisher, ReservationsDbContext dbContext)
    {
        _capPublisher = capPublisher;
        _dbContext = dbContext;
    }

    public async Task PublishChangedShowStatus(ChangedShowStatusEvent changedShowStatusEvent)
    {
        using(var transaction = _dbContext.Database.BeginTransaction(_capPublisher, true))
        {
            await _capPublisher.PublishAsync(ShowDomainEventsConstants.ChangedShowStatus, changedShowStatusEvent);
        }
    }
}

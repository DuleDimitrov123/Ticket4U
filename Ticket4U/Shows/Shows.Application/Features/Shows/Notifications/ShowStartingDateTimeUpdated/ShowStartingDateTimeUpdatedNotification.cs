using MediatR;
using Shared.Domain.Events;

namespace Shows.Application.Features.Shows.Notifications.ShowStartingDateTimeUpdated;

public record ShowStartingDateTimeUpdatedNotification(UpdatedShowsStartingDateTimeEvent updatedShowsStartingDateTimeEvent) : INotification;

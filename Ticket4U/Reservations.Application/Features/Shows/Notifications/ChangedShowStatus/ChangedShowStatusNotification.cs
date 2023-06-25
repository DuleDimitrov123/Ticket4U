using MediatR;
using Shared.Domain.Events;

namespace Reservations.Application.Features.Shows.Notifications.ChangedShowStatus;

public record ChangedShowStatusNotification(ChangedShowStatusEvent changedShowStatusEvent) : INotification;

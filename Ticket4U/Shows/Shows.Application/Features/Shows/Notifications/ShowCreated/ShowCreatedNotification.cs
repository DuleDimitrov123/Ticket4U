using MediatR;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Notifications.ShowCreated;

public record ShowCreatedNotification(Show Show) : INotification;

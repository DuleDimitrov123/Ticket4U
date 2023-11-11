using MediatR;
using Shared.Domain.Events;

namespace Users.Application.Features.Users.Notifications.UserCreated;

public record UserCreatedNotification(CreatedUserEvent CreatedUserEvent) : INotification;

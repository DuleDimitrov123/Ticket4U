using MediatR;
using Users.Application.Contracts.Outbox;

namespace Users.Application.Features.Users.Notifications.UserCreated;

public class UserCreatedNotificationHandler : INotificationHandler<UserCreatedNotification>
{
    private readonly IUserPublisher _userPublisher;

    public UserCreatedNotificationHandler(IUserPublisher userPublisher)
    {
        _userPublisher = userPublisher;
    }

    public async Task Handle(UserCreatedNotification userCreatedNotification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User created!: Email: {userCreatedNotification.CreatedUserEvent.Email} UserName: {userCreatedNotification.CreatedUserEvent.UserName}");

        await _userPublisher.PublishCreatedUser(userCreatedNotification.CreatedUserEvent);
    }
}

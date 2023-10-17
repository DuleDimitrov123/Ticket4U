namespace Shared.Domain.Events;

public class CreatedUserEvent : DomainEvent
{
    public CreatedUserEvent()
        : base()
    {

    }

    public CreatedUserEvent(string email, string userName)
    {
        Email = email;
        UserName = userName;
    }

    public string Email { get; set; }

    public string UserName { get; set; }
};

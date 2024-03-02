namespace Shared.Domain.Events;

public class CreatedUserEvent : DomainEvent
{
    public CreatedUserEvent()
        : base()
    {

    }

    public CreatedUserEvent(string email, string userName, Guid externalId)
    {
        Email = email;
        UserName = userName;
        ExternalId = externalId;
    }

    public string Email { get; set; }

    public string UserName { get; set; }

    public Guid ExternalId { get; set; }
};

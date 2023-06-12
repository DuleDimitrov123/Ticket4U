using Common;
using Shared.Domain;

namespace Shows.Domain.Shows;

public static class ShowStatusExtension
{
    public static ShowStatus Create(this ShowStatus status, string newStatus)
    {
        switch (newStatus) 
        {
            case "HasTickets":
                return ShowStatus.HasTickets;
            case "IsSoldOut":
                return ShowStatus.IsSoldOut;
            default:
                throw new DomainException(
                    new List<string>()
                    {
                        DefaultErrorMessages.ShowStatusDoesntExist.Replace("{status}", status.ToString())
                    });
        }
    }
}

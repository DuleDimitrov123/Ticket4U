using Common;
using Shows.Domain.Common;

namespace Shows.Domain.Shows;

public static class ShowStatusExtension
{
    public static ShowStatus Create(this ShowStatus status, string newStatus)
    {
        switch (newStatus) 
        {
            case "HasTickets":
                return ShowStatus.HasTickets;
            case "IsSoledOut":
                return ShowStatus.IsSoledOut;
            default:
                throw new DomainException(
                    new List<string>()
                    {
                        DefaultErrorMessages.ShowStatusDoesntExist.Replace("{status}", status.ToString())
                    });
        }
    }
}

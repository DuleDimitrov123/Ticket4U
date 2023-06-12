using Reservations.Common;
using Reservations.Common.Constants;
using Shared.Domain;
using System.Text.RegularExpressions;

namespace Reservations.Domain.Users;

/// <summary>
/// User that can make reservations
/// </summary>
public class User : AggregateRoot
{
    /// <summary>
    /// Email of the user that can make reservations
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// User's external id from users service
    /// </summary>
    public Guid ExternalId { get; private set; }

    private User(string email, Guid externalId)
    {
        Email = email;
        ExternalId = externalId;
    }

    public static User Create(string email, Guid externalId)
    {
        var errorMessages = new List<string>();

        ValidateUserCreation(email, externalId, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new User(email, externalId);
    }

    private static void ValidateUserCreation(string email, Guid externalId, List<string> errorMessages)
    {
        if (string.IsNullOrEmpty(email))
        {
            errorMessages.Add(DefaultErrorMessages.UserEmailIsRequired);
        }

        if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, UserConstants.EmailPattern, RegexOptions.IgnoreCase))
        {
            errorMessages.Add(DefaultErrorMessages.EmailNotValidFormat);
        }
    }
}

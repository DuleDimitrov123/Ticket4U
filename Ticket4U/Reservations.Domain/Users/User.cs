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
    /// Username from users service
    /// </summary>
    public string UserName { get; private set; }

    private User(string email, string userName)
    {
        Email = email;
        UserName = userName;
    }

    private User(Guid userId, string email, string userName)
    {
        Id = userId;
        Email = email;
        UserName = userName;
    }

    public static User Create(string email, string userName)
    {
        var errorMessages = new List<string>();

        ValidateUserCreation(email, userName, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new User(email, userName);
    }

    public static User Create(Guid userId, string email, string userName)
    {
        var errorMessages = new List<string>();

        ValidateUserCreation(userId, email, userName, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new User(userId, email, userName);
    }

    private static void ValidateUserCreation(string email, string userName, List<string> errorMessages)
    {
        if (string.IsNullOrEmpty(email))
        {
            errorMessages.Add(DefaultErrorMessages.UserEmailIsRequired);
        }

        if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, UserConstants.EmailPattern, RegexOptions.IgnoreCase))
        {
            errorMessages.Add(DefaultErrorMessages.EmailNotValidFormat);
        }

        if (string.IsNullOrEmpty(userName))
        {
            errorMessages.Add(DefaultErrorMessages.UserNameIsRequired);
        }
    }

    private static void ValidateUserCreation(Guid userId, string email, string userName, List<string> errorMessages)
    {
        if (userId == Guid.Empty)
        {
            errorMessages.Add(DefaultErrorMessages.CantCreateUserWithEmptyGuid);
        }

        ValidateUserCreation(email, userName, errorMessages);
    }
}

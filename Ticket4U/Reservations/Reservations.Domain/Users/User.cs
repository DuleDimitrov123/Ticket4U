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

    /// <summary>
    /// External id for user
    /// </summary>
    public Guid ExternalId { get; private set; }

    private User(string email, string userName, Guid externalId)
    {
        Email = email;
        UserName = userName;
        ExternalId = externalId;
    }

    private User(Guid userId, string email, string userName, Guid externalId)
    {
        Id = userId;
        Email = email;
        UserName = userName;
        ExternalId = externalId;
    }

    public static User Create(string email, string userName, Guid externalId)
    {
        var errorMessages = new List<string>();

        ValidateUserCreation(email, userName, externalId, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new User(email, userName, externalId);
    }

    public static User Create(Guid userId, string email, string userName, Guid externalId)
    {
        var errorMessages = new List<string>();

        ValidateUserCreation(userId, email, userName, externalId, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new User(userId, email, userName, externalId);
    }

    private static void ValidateUserCreation(string email, string userName, Guid externalId, List<string> errorMessages)
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

        if (externalId == Guid.Empty)
        {
            errorMessages.Add("ExternalId of user can't be guid empty!");
        }
    }

    private static void ValidateUserCreation(Guid userId, string email, string userName, Guid externalId, List<string> errorMessages)
    {
        if (userId == Guid.Empty)
        {
            errorMessages.Add(DefaultErrorMessages.CantCreateUserWithEmptyGuid);
        }

        ValidateUserCreation(email, userName, externalId, errorMessages);
    }
}

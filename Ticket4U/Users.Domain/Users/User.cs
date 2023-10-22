using Microsoft.AspNetCore.Identity;
using Shared.Domain;
using Users.Common;

namespace Users.Domain.Users;

public class User : IdentityUser
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public bool IsAdmin { get; private set; }

    private User()
    {

    }

    private User(string email, string username, string firstName, string lastName, bool emailConfirmed, bool isAdmin)
        : base()
    {
        Email = email;
        UserName = username;
        FirstName = firstName;
        LastName = lastName;
        EmailConfirmed = emailConfirmed;
        IsAdmin = isAdmin;
    }

    public static User Create(string email, string username, string firstName, string lastName, bool emailConfirmed, bool isAdmin)
    {
        IList<string> errorMessages = new List<string>();

        ValidateUserCreation(email, username, firstName, lastName, emailConfirmed, isAdmin, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new User(email, username, firstName, lastName, emailConfirmed, isAdmin);
    }

    private static void ValidateUserCreation(string email, string username, string firstName, string lastName, bool emailConfirmed, bool isAdmin, IList<string> errorMessages)
    {
        if (string.IsNullOrEmpty(email))
        {
            errorMessages.Add(DefaultErrorMessages.UserEmailRequired);
        }

        if (string.IsNullOrEmpty(username))
        {
            errorMessages.Add(DefaultErrorMessages.UserUsernameRequired);
        }

        if (string.IsNullOrEmpty(firstName))
        {
            errorMessages.Add(DefaultErrorMessages.UserFirstNameRequired);
        }

        if (string.IsNullOrEmpty(lastName))
        {
            errorMessages.Add(DefaultErrorMessages.UserLastNameRequired);
        }
    }
}

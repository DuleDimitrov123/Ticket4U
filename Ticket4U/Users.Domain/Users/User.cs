using Microsoft.AspNetCore.Identity;

namespace Users.Domain.Users;

//TODO: Maybe consider refactoring...implement other properties, do not inherit this class
public class User : IdentityUser
{
    public string FirstName { get; /*private*/ set; }

    public string LastName { get; /*private*/ set; }

    public bool IsAdmin { get; set; }

    //private User(string firstName, string lastName)
    //{
    //    FirstName = firstName;
    //    LastName = lastName;
    //}

    //public static User Create(string firstName, string lastName)
    //{
    //    IList<string> errorMessages = new List<string>();

    //    ValidateUserCreation(firstName, lastName, errorMessages);

    //    if (errorMessages.Count > 0)
    //    {
    //        throw new DomainException(errorMessages);
    //    }

    //    return new User(firstName, lastName);
    //}

    //private static void ValidateUserCreation(string firstName, string lastName, IList<string> errorMessages)
    //{
    //    if (string.IsNullOrEmpty(firstName))
    //    {
    //        errorMessages.Add("First name for the user cannot be null or empty");
    //    }

    //    if (string.IsNullOrEmpty(lastName))
    //    {
    //        errorMessages.Add("Last name for the user cannot be null or empty");
    //    }
    //}
}

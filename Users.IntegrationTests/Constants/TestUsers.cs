using Users.Application.Features.Users.Commands.RegistrateUser;

namespace Users.IntegrationTests.Constants;

public static class TestUsers
{
    public static RegistrateUserCommand TestAdmin = new RegistrateUserCommand
    {
        FirstName = "TestUserFirstName",
        LastName = "TestUserLastName",
        Email = "test@gmail.com",
        Password = "TestPass12*4NotReal",
        UserName = "TestUser",
        IsAdmin = true
    };
}

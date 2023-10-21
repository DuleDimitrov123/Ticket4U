using Users.Application.Models.Identity;

namespace Users.IntegrationTests.Constants;
public static class TestUsers
{
    public static RegistrationRequest TestAdmin = new RegistrationRequest
    {
        FirstName = "TestUserFirstName",
        LastName = "TestUserLastName",
        Email = "test@gmail.com",
        Password = "TestPass12*4NotReal",
        UserName = "TestUser"
    };
}

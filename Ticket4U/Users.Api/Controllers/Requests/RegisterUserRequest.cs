namespace Users.Api.Controllers.Requests;

public record RegisterUserRequest(string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password);

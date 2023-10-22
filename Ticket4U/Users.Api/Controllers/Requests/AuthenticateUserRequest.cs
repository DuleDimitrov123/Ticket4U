namespace Users.Api.Controllers.Requests;

public record AuthenticateUserRequest(string Email, string Password);

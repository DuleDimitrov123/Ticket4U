namespace Reservations.Api.Requests.Users;

public record CreateUserRequest(string Email, Guid ExternalId);

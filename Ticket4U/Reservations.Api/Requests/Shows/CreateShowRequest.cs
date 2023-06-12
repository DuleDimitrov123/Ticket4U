namespace Reservations.Api.Requests.Shows;

public record CreateShowRequest(string Name, DateTime StartingDateTime, int NumberOfPlaces, Guid ExternalId);

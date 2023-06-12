namespace Reservations.Api.Requests.Reservations;

public record CreateReservationRequest(Guid UserId, Guid ShowId, int NumberOfReservations);

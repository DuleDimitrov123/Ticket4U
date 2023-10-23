namespace Reservations.Application.Features.Reservations.Responses;

public class ReservationResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ShowId { get; set; }

    public int NumberOfReservations { get; set; }
}

namespace Reservations.Application.Features.Reservations.Responses;

public class ReservationResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ShowResponse Show { get; set; }

    public int NumberOfReservations { get; set; }
}

public class ShowResponse
{
    public Guid ShowId { get; set; }

    public string ShowName { get; set; }

    public DateTime ShowStartingDateTime { get; set; }

    public bool IsSoldOut { get; set; }
}

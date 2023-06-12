using Reservations.Common;
using Shared.Domain;

namespace Reservations.Domain.Reservations;

public class Reservation : AggregateRoot
{
    public Guid UserId { get; private set; }

    public Guid ShowId { get; private set; }

    public NumberOfReservations NumberOfReservations { get; private set; }

    private Reservation(Guid userId, Guid showId, NumberOfReservations numberOfReservations)
    {
        UserId = userId;
        ShowId = showId;
        NumberOfReservations = numberOfReservations;
    }

    public static Reservation Create(Guid userId, Guid showId, NumberOfReservations numberOfReservations)
    {
        IList<string> errorMessages = new List<string>();

        ValidateReservationCreation(userId, showId, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new Reservation(userId, showId, numberOfReservations);
    }

    private static void ValidateReservationCreation(Guid userId, Guid showId, IList<string> errorMessages)
    {
        if (userId == Guid.Empty)
        {
            errorMessages.Add(DefaultErrorMessages.UserIdForReservationRequired);
        }

        if(showId == Guid.Empty)
        {
            errorMessages.Add(DefaultErrorMessages.ShowIdForReservationRequired);
        }
    }
}

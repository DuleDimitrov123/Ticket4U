using Reservations.Common;
using Shared.Domain;
using System.Net.NetworkInformation;

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

    private Reservation(Guid reservationId, Guid userId, Guid showId, NumberOfReservations numberOfReservations)
    {
        Id = reservationId;
        UserId = userId;
        ShowId = showId;
        NumberOfReservations = numberOfReservations;
    }

    public static Reservation Create(Guid reservationId, Guid userId, Guid showId, NumberOfReservations numberOfReservations)
    {
        IList<string> errorMessages = new List<string>();

        ValidateReservationCreation(reservationId, userId, showId, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new Reservation(reservationId, userId, showId, numberOfReservations);
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

    public void UpdateNumberOfReservations(int newNumberOfReservations)
    {
        //NumberOfReservations is ValueObject, don't update, but create new!
        NumberOfReservations = NumberOfReservations.Create(newNumberOfReservations);
    }

    private static void ValidateReservationCreation(Guid reservationId, Guid userId, Guid showId, IList<string> errorMessages)
    {
        if (reservationId == Guid.Empty)
        {
            errorMessages.Add(DefaultErrorMessages.CantCreateReservationWithEmptyGuid);
        }

        ValidateReservationCreation(userId, showId, errorMessages);
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

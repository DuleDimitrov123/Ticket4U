using Reservations.Common;
using Reservations.Common.Constants;
using Shared.Domain;

namespace Reservations.Domain.Reservations;

public record NumberOfReservations : ValueObject
{
    public int Value { get; private set; }

    private NumberOfReservations(int value)
    {
        Value = value;
    }

    public static NumberOfReservations Create(int numberOfReservations)
    {
        IList<string> errorMessages = new List<string>();

        //first check if numberOfReservations is > 0
        if (numberOfReservations <= 0)
        {
            errorMessages.Add(DefaultErrorMessages.NumberOfReservationsGreaterThan0);
        }

        //one user cannot make more reservations than ReservationConstants.MaxNumberOfReservationsPerUser
        if (numberOfReservations > ReservationConstants.MaxNumberOfReservationsPerUser)
        {
            errorMessages.Add($"{DefaultErrorMessages.NumberOfReservationsGreaterThanLimit} {ReservationConstants.MaxNumberOfReservationsPerUser}.");
        }

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new NumberOfReservations(numberOfReservations);
    }
}

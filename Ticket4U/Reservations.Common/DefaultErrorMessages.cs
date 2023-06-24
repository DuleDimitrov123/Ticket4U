namespace Reservations.Common;

public static class DefaultErrorMessages
{
    public static readonly string CantGetByEmptyGuid = "You cannot get objects with empty guid!";

    public static readonly string UserEmailIsRequired = "User emailis required!";

    public const string EmailNotValidFormat = "Email must be in valid format.";

    public static readonly string ShowStartingDateTimeNotInFuture = "Show starting DateTime needs to be in the future!";

    public static readonly string ShowNameIsRequired = "Show name is required!";

    public static readonly string ShowNameLength = "Show name is to long!";

    public static readonly string NumberOfPlacesGreaterThan0 = "Number of places need to be greated than 0!";

    public static readonly string NumberOfReservationsGreaterThan0 = "Number of reservations need to be greated than 0!";

    public static readonly string NumberOfReservationsGreaterThanLimit = "One user cannot make more resrevations than ";

    public static readonly string CantCreateReservationWithEmptyGuid = "Can't create reservation with empty guid!";

    public static readonly string UserIdForReservationRequired = "UserId for reservation is required!";

    public static readonly string CantCreateUserWithEmptyGuid = "Can't create user with empty guid!";

    public static readonly string ShowIdForReservationRequired = "ShowId for reservation is required!";
}

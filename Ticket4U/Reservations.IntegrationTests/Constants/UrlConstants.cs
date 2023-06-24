namespace Reservations.IntegrationTests.Constants;

public static class UrlConstants
{
    #region ReservationConstants

    public const string BaseReservationURL = "/api/reservations";

    public const string GetReservationByUserIdSpecificURL = "/user";

    public const string UpdateNumberOfReservationsSpecificURL = "/newNumberOfResevations";

    #endregion

    #region ShowConstants

    public const string BaseShowURL = "/api/shows";

    public const string CreateShowSpecificURL = "CAPROUTE-CreateShowInReservations";

    public const string UpdateShowStartingDateTimeSpecificURL = "CAPROUTE-UpdateShowStartingDateTime";

    #endregion

    #region UserConstants

    public const string BaseUserURL = "/api/users";

    public const string CreateUserSpecificURL = "CAPROUTE-CreateUserInReservations";

    #endregion
}

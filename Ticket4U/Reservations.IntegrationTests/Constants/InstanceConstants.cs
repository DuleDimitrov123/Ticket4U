namespace Reservations.IntegrationTests.Constants;

public static class InstanceConstants
{
    private static Guid _show1Id = Guid.NewGuid();

    private static Guid _externalShow1Id = Guid.NewGuid();

    private static Guid _user1Id = Guid.NewGuid();

    private static Guid _externalUser1Id = Guid.NewGuid();

    private static Guid _reservationId1 = Guid.NewGuid();

    public static Guid Show1Id
    {
        get
        {
            return _show1Id;
        }
    }

    public static Guid ExternalShow1Id
    {
        get
        {
            return _externalShow1Id;
        }
    }

    public static Guid User1Id
    {
        get
        {
            return _user1Id;
        }
    }

    public static Guid ExternalUser1Id
    {
        get
        {
            return _externalUser1Id;
        }
    }

    public static Guid ReservationId1
    {
        get
        {
            return _reservationId1;
        }
    }
}

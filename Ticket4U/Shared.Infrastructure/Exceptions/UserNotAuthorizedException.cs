namespace Shared.Infrastructure.Exceptions;

public class UserNotAuthorizedException : Exception
{
    public UserNotAuthorizedException()
        : base("User is not authorized")
    {
    }
}

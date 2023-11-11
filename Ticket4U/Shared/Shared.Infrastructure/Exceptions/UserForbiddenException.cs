namespace Shared.Infrastructure.Exceptions;

public class UserForbiddenException : Exception
{
    public UserForbiddenException()
        : base("User doesn't have enough privileges for this endpoint!")
    {

    }
}

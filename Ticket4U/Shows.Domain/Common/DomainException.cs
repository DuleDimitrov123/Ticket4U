namespace Shows.Domain.Common;

public class DomainException : Exception
{
    public DomainException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    public IList<string> ErrorMessages { get; set; }
}

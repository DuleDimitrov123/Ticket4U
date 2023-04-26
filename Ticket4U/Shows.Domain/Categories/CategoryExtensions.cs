using Common;
using Shows.Domain.Common;

namespace Shows.Domain.Categories;

public static class CategoryExtensions
{
    public static CategoryStatus Create(this CategoryStatus status, string newStatus)
    {
        switch (newStatus) 
        { 
            case "IsValid":
                return CategoryStatus.IsValid;
            case "IsArchived":
                return CategoryStatus.IsArchived;
            default:
                throw new DomainException(
                    new List<string>() 
                    { 
                        DefaultErrorMessages.CategoryStatusDoesntExist.Replace("{status}", status.ToString()) 
                    });
        }
    }
}

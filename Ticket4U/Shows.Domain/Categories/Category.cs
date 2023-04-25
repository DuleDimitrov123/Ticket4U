using Common;
using Common.Constants;
using Shows.Domain.Common;

namespace Shows.Domain.Categories;

public class Category : AggregateRoot
{
    public string Name { get; set; }

    public string Description { get; set; }

    private Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Category Create(string name, string description)
    {
        var errorMessages = new List<string>();

        ValidateCategory(name, description, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new Category(name, description);
    }

    private static void ValidateCategory(string name, string description, List<string> errorMessages)
    {
        if (string.IsNullOrEmpty(name))
        {
            errorMessages.Add(DefaultErrorMessages.CategoryNameIsRequired);
        }

        if (name.Length > CategoryConstants.CategoryNameMaxLength)
        {
            errorMessages.Add(DefaultErrorMessages.CategoryNameLength);
        }

        if (string.IsNullOrEmpty(description))
        {
            errorMessages.Add(DefaultErrorMessages.CategoryDescriptionIsRequired);
        }
    }
}

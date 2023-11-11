using Common;
using Common.Constants;
using Shared.Domain;

namespace Shows.Domain.Categories;

public class Category : AggregateRoot
{
    public string Name { get; private set; }

    public string Description { get; private set; }

    public CategoryStatus Status { get; private set; }

    private Category(string name, string description)
    {
        Name = name;
        Description = description;
        Status = CategoryStatus.IsValid;
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

    public void UpdateCategoryName(string newName)
    {
        var errorMessages = new List<string>();

        if (Status == CategoryStatus.IsArchived)
        {
            errorMessages.Add(DefaultErrorMessages.CantUpdateArchivedCategory);
            throw new DomainException(errorMessages);
        }

        ValidateCategoryName(newName, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        Name = newName;
    }

    public void UpdateCategoryDescription(string newDescription)
    {
        var errorMessages = new List<string>();

        if (Status == CategoryStatus.IsArchived)
        {
            errorMessages.Add(DefaultErrorMessages.CantUpdateArchivedCategory);
            throw new DomainException(errorMessages);
        }

        ValidateCategoryDescription(newDescription, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        Description = newDescription;
    }

    public void ArchiveCategory()
    {
        Status = CategoryStatus.IsArchived;
    }

    private static void ValidateCategory(string name, string description, IList<string> errorMessages)
    {
        ValidateCategoryName(name, errorMessages);

        ValidateCategoryDescription(description, errorMessages);
    }

    private static void ValidateCategoryName(string name, IList<string> errorMessages)
    {
        if (string.IsNullOrEmpty(name))
        {
            errorMessages.Add(DefaultErrorMessages.CategoryNameIsRequired);
        }

        if (name?.Length > CategoryConstants.CategoryNameMaxLength)
        {
            errorMessages.Add(DefaultErrorMessages.CategoryNameLength);
        }
    }

    private static void ValidateCategoryDescription(string description, IList<string> errorMessages)
    {
        if (string.IsNullOrEmpty(description))
        {
            errorMessages.Add(DefaultErrorMessages.CategoryDescriptionIsRequired);
        }
    }
}

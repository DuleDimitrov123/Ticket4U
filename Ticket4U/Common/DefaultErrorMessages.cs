namespace Common;

public static class DefaultErrorMessages
{
    public static readonly string CantGetByEmptyGuid = "You cannot get objects with empty guid!";

    public static readonly string PerformerNameIsRequired = "Performer name is required!";

    public static readonly string PerformerNameLength = "Performer name is to long!";

    public static readonly string PerformerInfoNameRequired = "Performer info name is required!";

    public static readonly string PerformerInfoValueRequired = "Performer info value is required!";

    public static readonly string CategoryNameIsRequired = "Category name is required!";

    public static readonly string CategoryDescriptionIsRequired = "Category description is required!";

    public static readonly string CategoryNameLength = "Category name is to long!";

    public static readonly string CantUpdateArchivedCategory = "Archived categories cannot be updated!";

    public static readonly string CategoryStatusDoesntExist = $"Category status {0} doesn't exist";
}

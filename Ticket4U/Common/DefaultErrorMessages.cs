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

    public static readonly string ShowNameIsRequired = "Show name is required!";

    public static readonly string ShowNameLength = "Show name is to long!";

    public static readonly string ShowDescriptionIsRequired = "Show description is required!";

    public static readonly string ShowDescriptionLength = "Show description is to long!";

    public static readonly string ShowPictureIsRequired = "Show picture is to long!";

    public static readonly string ShowLocationIsRequired = "Show location is required!";

    public static readonly string ShowStatusIsRequired = "Show status is required!";

    public static readonly string ShowLocationLength = "Show location is to long!";

    public static readonly string ShowStartingDateTimeNotInFuture = "Show starting DateTime needs to be in the future!";

    public static readonly string NumberOfPlacesGreaterThan0 = "Number of places need to be greated than 0!";

    public static readonly string MoneyCurrencyRequired = "Money currency is required!";

    public static readonly string MoneyCurrencyLength = "Money currency has invalid format. Format requires only 3 characters!";

    public static readonly string MoneyAmountNotNegative = "Money amount cannot be negative!";

    public static readonly string ShowPerformerIdRequired = "Show needs to have the performer!";

    public static readonly string ShowCategoryIdRequired = "Show needs to have the category!";

    public static readonly string ShowStatusDoesntExist = $"Show status {0} doesn't exist";

    public static readonly string ShowMessageNameRequired = "Show message name is required!";

    public static readonly string ShowMessageNameLength = "Show message name is to long!";

    public static readonly string ShowMessageValueRequired = "Show message value is required!";

    public static readonly string ShowMessageValueLength = "Show message value is to long!";

    public static readonly string ShowMessageShowIdRequired = "Show message show id is required!";
}

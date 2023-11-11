namespace Shows.IntegrationTests.Constants;

public static class UrlConstants
{
    #region CategoriesConstants

    public const string BaseCategoryURL = "/api/categories";

    public const string SpecificArchiveCategoryURL = "archive";

    #endregion

    #region PerformersController

    public const string BasePerformerURL = "/api/performers";

    public const string SpecificPerformerDetail = "detail";

    public const string SpecificUpdatePerformerInfo = "performer-info";

    #endregion

    #region ShowsController

    public const string BaseShowURL = "/api/shows";

    public const string SpecificShowDetail = "detail";

    public const string SpecificUpdateShowName = "newName";

    public const string SpecificUpdateShowLocation = "newLocation";

    public const string SpecificUpdateShowPrice = "newPrice";

    public const string SpecificUpdateShowStartingDateTime = "newStartingDateTime";

    public const string SpecificAddShowMessage = "showMessages";

    #endregion
}

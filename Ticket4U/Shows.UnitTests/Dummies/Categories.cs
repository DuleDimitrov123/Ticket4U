using Shows.Domain.Categories;

namespace Shows.UnitTests.Dummies;

public static class Categories
{
    public static Category Category1 = Category.Create("Category1", "Description of category 1");

    public static Guid Category1Id = Guid.NewGuid();

    public static Category Category2 = Category.Create("Category2", "Description of category 2");

    public static Guid Category2Id = Guid.NewGuid();

    public static Category Category3 = Category.Create("Category3", "Description of category 3");

    public static Guid Category3Id = Guid.NewGuid();

    public static IList<Category> CategoriesList = new List<Category>()
    {
        Shows.UnitTests.Dummies.Categories.Category1,
        Shows.UnitTests.Dummies.Categories.Category2,
        Shows.UnitTests.Dummies.Categories.Category3
    };
}

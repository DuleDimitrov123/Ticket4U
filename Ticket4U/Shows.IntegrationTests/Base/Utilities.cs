using Shows.Domain.Categories;
using Shows.Infrastructure.Persistance;

namespace Shows.IntegrationTests.Base;

public class Utilities
{
    public static void InitializeDbForTestsAsync(AppDbContext context)
    {
        context.Categories.Add(Category.Create("Category1", "Description of category 1"));
        context.Categories.Add(Category.Create("Category2", "Description of category 2"));
        context.Categories.Add(Category.Create("Category3", "Description of category 3"));
        context.SaveChangesAsync();
    }
}

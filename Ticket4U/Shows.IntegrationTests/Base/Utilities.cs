using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Infrastructure.Persistance;

namespace Shows.IntegrationTests.Base;

public class Utilities
{
    public static void InitializeDbForTestsAsync(AppDbContext context)
    {
        context.Categories.Add(Category.Create("Category1", "Description of category 1"));
        context.Categories.Add(Category.Create("Category2", "Description of category 2"));
        context.Categories.Add(Category.Create("Category3", "Description of category 3"));

        context.Performers.Add(Performer.Create("Performer1",
            new List<PerformerInfo>()
            {
                PerformerInfo.Create("DateOfBirth", "01.01.1999."),
                PerformerInfo.Create("Number of concerts", "100"),
            }));

        context.Performers.Add(Performer.Create("Performer2",
            new List<PerformerInfo>()
            {
                        PerformerInfo.Create("DateOfBirth", "13.01.1996.")
            }));
        context.SaveChangesAsync();
    }
}

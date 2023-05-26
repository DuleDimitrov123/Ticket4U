﻿using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;
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

        context.Shows.Add(
            Show.Create("Test Show", "Test location", NumberOfPlaces.Create(100),
            Money.Create("RSD", 100),
            DateTime.Now.AddDays(30),
            GetPerformerByName("Performer1", context)!.Id,
            GetCategoryByName("Category1", context)!.Id));

        context.SaveChangesAsync();
    }

    public static Performer? GetPerformerByName(string name, AppDbContext context)
    {
        return context.Performers.Where(p => p.Name == name).FirstOrDefault();
    }

    public static Category? GetCategoryByName(string name, AppDbContext context)
    {
        return context.Categories.Where(c => c.Name == name).FirstOrDefault();
    }
}

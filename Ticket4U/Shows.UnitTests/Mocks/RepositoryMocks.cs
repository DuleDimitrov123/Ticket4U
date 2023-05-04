using Moq;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Categories;

namespace Shows.UnitTests.Mocks;

public static class RepositoryMocks
{
    private static IList<Category> categories;

    static RepositoryMocks()
    {
        InitCategories();
    }

    public static void InitCategories()
    {
        categories = Shows.UnitTests.Dummies.Categories.CategoriesList;
    }

    public static IMock<IRepository<Category>> InitCategoryMockRepository()
    {
        var mockCategoryRepository = new Mock<IRepository<Category>>();
        mockCategoryRepository.Setup(repo => repo.GetAll()).ReturnsAsync(
            new List<Category>()
            {
                Shows.UnitTests.Dummies.Categories.Category1,
                Shows.UnitTests.Dummies.Categories.Category2,
                Shows.UnitTests.Dummies.Categories.Category3
            });

        mockCategoryRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(Shows.UnitTests.Dummies.Categories.Category1);

        mockCategoryRepository.Setup(repo => repo.Add(It.IsAny<Category>()))
            .ReturnsAsync(
                (Category category) =>
                {
                    categories.Add(category);
                    return category;
                });

        //Not sure if setting it up correctly
        //mockCategoryRepository.Setup(repo => repo.Update(It.IsAny<Category>()));

        return mockCategoryRepository;
    }
}

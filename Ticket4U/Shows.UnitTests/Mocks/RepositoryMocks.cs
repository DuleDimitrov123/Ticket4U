using Moq;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Categories;

namespace Shows.UnitTests.Mocks;

public static class RepositoryMocks
{
    public static IMock<IRepository<Category>> InitCategoryMockRepository()
    {
        var mockCategoryRepository = new Mock<IRepository<Category>>();

        var categories = new List<Category>()
        {
            Category.Create("Category1", "Description of category 1"),
            Category.Create("Category2", "Description of category 2"),
            Category.Create("Category3", "Description of category 3")
        };

        mockCategoryRepository.Setup(repo => repo.GetAll()).ReturnsAsync(
            categories);

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

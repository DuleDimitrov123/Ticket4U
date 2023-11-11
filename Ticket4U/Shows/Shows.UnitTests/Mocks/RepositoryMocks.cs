using Moq;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Categories;
using Shows.Domain.Performers;

namespace Shows.UnitTests.Mocks;

public static class RepositoryMocks
{
    public static IMock<IRepository<Performer>> InitPerformerMockRepository()
    {
        var mockPerformerRepository = new Mock<IRepository<Performer>>();

        var performers = new List<Performer>()
        {
            Performer.Create("Performer1", new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName1", "PerformerInfoValue1")
            }),
            Performer.Create("Performer2", new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName2", "PerformerInfoValue2")
            }),
            Performer.Create("Performer3", new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName3", "PerformerInfoValue3")
            })
        };

        mockPerformerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(performers);

        mockPerformerRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(Shows.UnitTests.Dummies.Performers.Performer1);

        mockPerformerRepository.Setup(repo => repo.Add(It.IsAny<Performer>()))
            .ReturnsAsync(
                (Performer performer) =>
                {
                    performers.Add(performer);
                    return performer;
                });

        return mockPerformerRepository;
    }

    public static IMock<IPerformerRepository> InitSpecificMockPerformerRepository()
    {
        var mockSpecificPerformerRepository = new Mock<IPerformerRepository>();

        var performers = new List<Performer>()
        {
            Performer.Create("Performer1", new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName1", "PerformerInfoValue1")
            }),
            Performer.Create("Performer2", new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName2", "PerformerInfoValue2")
            }),
            Performer.Create("Performer3", new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName3", "PerformerInfoValue3")
            })
        };

        mockSpecificPerformerRepository.Setup(repo => repo.GetPerformerWithPerformerInfos(It.IsAny<Guid>()))
            .ReturnsAsync(performers[0]);

        return mockSpecificPerformerRepository;
    }

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

using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shouldly;
using Shows.Application.Features.Categories.Commands.CreateCategory;
using Shows.Application.Profiles;
using Shows.Domain.Categories;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.Categories.Commands;

public class CreateCategoryCommandHandlerTests
{
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Theory]
    [AutoMoqData]
    public async Task CreateCategoryCommandHandlerTest([Frozen] Mock<ICommandRepository<Category>> mockCommandRepository,
        CreateCategoryCommandHandler handler)
    {
        var categories = new List<Category>()
        {
            Category.Create("Category1", "Description of category 1"),
            Category.Create("Category2", "Description of category 2"),
            Category.Create("Category3", "Description of category 3")
        };

        mockCommandRepository.Setup(repo => repo.Add(It.IsAny<Category>()))
            .ReturnsAsync(
                (Category category) =>
                {
                    categories.Add(category);
                    return category;
                });

        handler = new CreateCategoryCommandHandler(_mapper, mockCommandRepository.Object);

        var newCategory = new CreateCategoryCommand()
        {
            Name = "TestCategoryName",
            Description = "TestCategoryDescription"
        };

        await handler.Handle(newCategory,
            CancellationToken.None);

        categories.Count.ShouldBe(4);
        categories.Select(c => c.Name).ShouldContain(newCategory.Name);
        categories.Select(c => c.Description).ShouldContain(newCategory.Description);
    }
}

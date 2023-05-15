using Moq;
using Shouldly;
using Shows.Application.Features.Categories.Commands.CreateCategory;
using Shows.UnitTests.Mocks;

namespace Shows.UnitTests.Categories.Commands;

public class CreateCategoryCommandHandlerTests : CategoriesQueryCommandHandlerTestBase
{
    public CreateCategoryCommandHandlerTests()
        : base()
    {
        
    }

    [Fact]
    public async Task CreateCategoryCommandHandlerTest()
    {
        var handler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

        await handler.Handle(
            new CreateCategoryCommand()
            {
                Name = "TestCategoryName",
                Description = "TestCategoryDescription"
            },
            CancellationToken.None);

        var allCategories = await _mockCategoryRepository.Object.GetAll();
        allCategories.Count.ShouldBe(4);
    }
}

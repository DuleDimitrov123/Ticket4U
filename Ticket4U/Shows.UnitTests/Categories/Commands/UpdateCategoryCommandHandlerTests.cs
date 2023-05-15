using Moq;
using Shows.Application.Features.Categories.Commands.UpdateCategory;

namespace Shows.UnitTests.Categories.Commands;

public class UpdateCategoryCommandHandlerTests : CategoriesQueryCommandHandlerTestBase
{
    public UpdateCategoryCommandHandlerTests()
        : base()
    {
        
    }

    [Fact]
    public async Task UpdateCategoryCommandHandlerTest()
    {
        var handler = new UpdateCategoryCommandHandler(_mockCategoryRepository.Object);

        await handler.Handle(
            new UpdateCategoryCommand()
            {
                CategoryId = Guid.NewGuid(),
                NewName = "NewCategoryName",
                NewDescription = "NewCategoryDescription",
            },
            CancellationToken.None);

        //Verify cannot be found
        //_mockCategoryRepository.Verify(d => d.Update(It.IsAny<App>()), Times.Once());
    }
}

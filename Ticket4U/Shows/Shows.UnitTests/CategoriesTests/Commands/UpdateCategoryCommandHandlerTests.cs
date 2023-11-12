using Moq;
using Shared.Application.Contracts.Persistence;
using Shows.Application.Features.Categories.Commands.UpdateCategory;
using Shows.Domain.Categories;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.Categories.Commands;

public class UpdateCategoryCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task UpdateCategoryCommandHandlerTest(Mock<ICommandRepository<Category>> mockCommandRepository,
        Mock<IQueryRepository<Category>> mockQueryRepository,
        UpdateCategoryCommandHandler handler)
    {
        var category = Category.Create("Category1", "Description of category 1");

        mockQueryRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(category);

        handler = new UpdateCategoryCommandHandler(mockCommandRepository.Object, mockQueryRepository.Object);

        var editedCategory = new UpdateCategoryCommand()
        {
            CategoryId = Guid.NewGuid(),
            NewName = "NewCategoryName",
            NewDescription = "NewCategoryDescription",
        };

        await handler.Handle(editedCategory,
            CancellationToken.None);

        mockCommandRepository.Verify(d => d.Update(It.IsAny<Category>()), Times.Once());
    }
}

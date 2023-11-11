using Shouldly;
using Shows.Application.Features.Categories.Queries.GetCategoryById;

namespace Shows.UnitTests.Categories.Queries;

public class GetCategoryByIdQueryHandlerTests : CategoriesQueryCommandHandlerTestBase
{
    public GetCategoryByIdQueryHandlerTests()
        : base()
    {
        
    }

    [Fact]
    public async Task GetCategoryByIdTest()
    {
        var handler = new GetCategoryByIdQueryHandler(_mapper, _mockCategoryRepository.Object);

        var result = await handler.Handle(
            new GetCategoryByIdQuery()
            {
                CategoryId = Guid.Empty
            },
            CancellationToken.None);

        result.ShouldNotBeNull();

        result.Name.ShouldBe(Shows.UnitTests.Dummies.Categories.Category1.Name);
        result.Description.ShouldBe(Shows.UnitTests.Dummies.Categories.Category1.Description);
    }
}

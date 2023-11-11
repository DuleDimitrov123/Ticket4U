using Shouldly;
using Shows.Application.Features.Categories.Queries;
using Shows.Application.Features.Categories.Queries.GetCategories;

namespace Shows.UnitTests.Categories.Queries;

public class GetCategoriesQueryHandlerTests : CategoriesQueryCommandHandlerTestBase
{
    public GetCategoriesQueryHandlerTests()
        : base()
    {
    }

    [Fact]
    public async Task GetCategoriesListTest()
    {
        var handler = new GetCategoriesQueryHandler(_mapper, _mockCategoryRepository.Object);

        var result = await handler.Handle(new GetCategoriesQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<CategoryResponse>>();

        result.Count.ShouldBe(3);
    }
}

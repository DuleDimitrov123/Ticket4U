using Shows.Application.Features.Categories.Queries.GetCategories;
using Shows.Application.Features.Categories.Queries;
using Shows.Application.Features.Performers.Queries.GetPerformers;
using Shouldly;
using Shows.Application.Features.Performers.Queries;

namespace Shows.UnitTests.Performers.Queries;

public class GetPerformersQueryHandlerTests : PerformersQueryCommandHandlerTestBase
{
    [Fact]
    public async Task GetPerformersListTest()
    {
        var handler = new GetPerformersQueryHandler(_mapper, _mockPerformerRepository.Object);

        var result = await handler.Handle(new GetPerformersQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<PerformerResponse>>();

        result.Count.ShouldBe(3);
    }
}

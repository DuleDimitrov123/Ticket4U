using Shouldly;
using Shows.Application.Features.Performers.Queries.GetPerformerById;

namespace Shows.UnitTests.Performers.Queries;

public class GetPerformerByIdQueryHandlerTests : PerformersQueryCommandHandlerTestBase
{
    [Fact]
    public async Task GetPerformerByIdTest()
    {
        var handler = new GetPerformerByIdQueryHandler(_mapper, _mockPerformerRepository.Object);

        var result = await handler.Handle(
            new GetPerformerByIdQuery()
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None);

        result.ShouldNotBeNull();

        result.Name.ShouldBe(Shows.UnitTests.Dummies.Performers.Performer1.Name);
    }
}

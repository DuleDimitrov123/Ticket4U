using Shouldly;
using Shows.Application.Features.Categories.Commands.CreateCategory;
using Shows.Application.Features.Performers.Commands.CreatePerformer;

namespace Shows.UnitTests.Performers.Commands;

public class CreatePerformerCommandHandlerTests : PerformersQueryCommandHandlerTestBase
{
    [Fact]
    public async Task CreatePerformerCommandHandlerTest()
    {
        var handler = new CreatePerformerCommandHandler(_mapper, _mockPerformerRepository.Object);

        await handler.Handle(
            new CreatePerformerCommand()
            {
                Name = "NewPerformer"
            },
            CancellationToken.None);

        var allPerformers = await _mockPerformerRepository.Object.GetAll();
        allPerformers.Count.ShouldBe(4);
    }
}

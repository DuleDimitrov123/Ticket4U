using AutoFixture.Xunit2;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shouldly;
using Shows.Application.Features.Performers.Commands.CreatePerformer;
using Shows.Domain.Performers;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.Performers.Commands;

public class CreatePerformerCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task CreatePerformerCommandHandlerTest([Frozen] Mock<ICommandRepository<Performer>> commandMockRepository,
        CreatePerformerCommandHandler handler)
    {
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

        commandMockRepository.Setup(repo => repo.Add(It.IsAny<Performer>()))
            .ReturnsAsync(
                (Performer performer) =>
                {
                    performers.Add(performer);
                    return performer;
                });

        var newPerformer = new CreatePerformerCommand()
        {
            Name = "NewPerformer"
        };

        await handler.Handle(
            newPerformer,
            CancellationToken.None);

        performers.Count.ShouldBe(4);
        performers.Select(p => p.Name).ShouldContain(newPerformer.Name);
    }
}

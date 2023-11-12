using AutoFixture.Xunit2;
using MediatR;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Commands.UpdateShowStartingDateTime;
using Shows.Domain.Shows;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.ShowsTests.Commands;

public class UpdateShowStartingDateTimeCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task UpdateShowStartingDateTime([Frozen] Mock<IMediator> mediator,
        [Frozen] Mock<IShowQueryRepository> mockQueryRepository,
        [Frozen] Mock<ICommandRepository<Show>> mockCommandRepository,
        UpdateShowStartingDateTimeCommandHandler handler)
    {
        //arrange
        var initialShowStartingDateTime = DateTime.Now.AddDays(30);
        var updatedShowStartingDateTime = DateTime.Now.AddDays(60);

        var show = Show.Create("ShowName", "ShowDescription", "ShowPictureBase64", "ShowLocation", NumberOfPlaces.Create(100),
            Money.Create("rsd", 50), initialShowStartingDateTime, Guid.NewGuid(), Guid.NewGuid());
        var showPropertyInfo = typeof(Show).GetProperty("Id");
        showPropertyInfo!.SetValue(show, Guid.NewGuid());

        show.AddShowMessage("ShowMessage1Name", "ShowMessage1Value");

        mockQueryRepository.Setup(x => x.GetShowWithShowMessages(It.IsAny<Guid>())).ReturnsAsync(show);

        handler = new UpdateShowStartingDateTimeCommandHandler(mediator.Object, mockQueryRepository.Object, mockCommandRepository.Object);

        var command = new UpdateShowStartingDateTimeCommand() { ShowId = show.Id, NewStartingDateTime = updatedShowStartingDateTime };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        mockCommandRepository.Verify(x => x.Update(It.Is<Show>(x => x.Id == show.Id)), Times.Once());
    }
}

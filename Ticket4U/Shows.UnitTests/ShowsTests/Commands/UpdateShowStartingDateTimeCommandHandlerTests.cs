using MediatR;
using Moq;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Commands.UpdateShowStartingDateTime;
using Shows.Domain.Shows;

namespace Shows.UnitTests.ShowsTests.Commands;

public class UpdateShowStartingDateTimeCommandHandlerTests
{
    [Fact]
    public async Task UpdateShowStartingDateTime()
    {
        //arrange
        var initialShowStartingDateTime = DateTime.Now.AddDays(30);
        var updatedShowStartingDateTime = DateTime.Now.AddDays(60);

        var show = Show.Create("ShowName", "ShowLocation", NumberOfPlaces.Create(100),
            Money.Create("rsd", 50), initialShowStartingDateTime, Guid.NewGuid(), Guid.NewGuid());
        var showPropertyInfo = typeof(Show).GetProperty("Id");
        showPropertyInfo!.SetValue(show, Guid.NewGuid());

        show.AddShowMessage("ShowMessage1Name", "ShowMessage1Value");

        var showsMockRepository = new Mock<IShowRepository>();
        showsMockRepository.Setup(x => x.GetShowWithShowMessages(It.IsAny<Guid>())).ReturnsAsync(show);

        var handler = new UpdateShowStartingDateTimeCommandHandler(showsMockRepository.Object, new Mock<IMediator>().Object);

        var command = new UpdateShowStartingDateTimeCommand() { ShowId = show.Id, NewStartingDateTime = updatedShowStartingDateTime };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        showsMockRepository.Verify(x => x.Update(It.Is<Show>(x => x.Id == show.Id)), Times.Once());
    }
}

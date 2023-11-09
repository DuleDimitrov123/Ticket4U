using Moq;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Commands.UpdateShowLocation;
using Shows.Domain.Shows;

namespace Shows.UnitTests.ShowsTests.Commands;

public class UpdateShowLocationCommandHandlerTests
{
    [Fact]
    public async Task UpdateShowLocation()
    {
        //arrange
        var initialShowLocation = "ShowLocation";
        var updatedShowLocation = "UpdatedShowLocation";

        var show = Show.Create("ShowName", "ShowDescription", initialShowLocation, NumberOfPlaces.Create(100),
            Money.Create("rsd", 100), DateTime.Now.AddDays(10), Guid.NewGuid(), Guid.NewGuid());
        var showPropertyInfo = typeof(Show).GetProperty("Id");
        showPropertyInfo!.SetValue(show, Guid.NewGuid());

        var showsMockRepository = new Mock<IRepository<Show>>();
        showsMockRepository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        var handler = new UpdateShowLocationCommandHandler(showsMockRepository.Object);

        var command = new UpdateShowLocationCommand() { Id = show.Id, NewLocation = updatedShowLocation };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        showsMockRepository.Verify(x => x.Update(It.Is<Show>(x => x.Id == show.Id)), Times.Once());
    }
}

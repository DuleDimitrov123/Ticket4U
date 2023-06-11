using Moq;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Commands.UpdateShowName;
using Shows.Domain.Performers;
using Shows.Domain.Shows;

namespace Shows.UnitTests.ShowsTests.Commands;

public class UpdateShowNameCommandHandlerTests
{
    [Fact]
    public async Task UpdateShowName()
    {
        //arrange
        var initialShowName = "ShowName";
        var updatedShowName = "UpdatedShowName";

        var show = Show.Create(initialShowName, "ShowLocation", NumberOfPlaces.Create(100),
            Money.Create("rsd", 100), DateTime.Now.AddDays(10), Guid.NewGuid(), Guid.NewGuid());
        var showPropertyInfo = typeof(Show).GetProperty("Id");
        showPropertyInfo!.SetValue(show, Guid.NewGuid());

        var showsMockRepository = new Mock<IRepository<Show>>();
        showsMockRepository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        var handler = new UpdateShowNameCommandHandler(showsMockRepository.Object);

        var command = new UpdateShowNameCommand() { Id = show.Id, NewName = updatedShowName };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        showsMockRepository.Verify(x => x.Update(It.Is<Show>(x => x.Id == show.Id)), Times.Once());
    }
}

using Moq;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Commands.UpdateShowPrice;
using Shows.Domain.Shows;

namespace Shows.UnitTests.ShowsTests.Commands;

public class UpdateShowPriceCommandHandlerTests
{
    [Fact]
    public async Task UpdateShowPrice()
    {
        //arrange
        var initialShowPrice = 50;
        var updatedShowPrice = 150;

        var show = Show.Create("ShowName", "ShowLocation", NumberOfPlaces.Create(100),
            Money.Create("rsd", initialShowPrice), DateTime.Now.AddDays(10), Guid.NewGuid(), Guid.NewGuid());
        var showPropertyInfo = typeof(Show).GetProperty("Id");
        showPropertyInfo!.SetValue(show, Guid.NewGuid());

        var showsMockRepository = new Mock<IRepository<Show>>();
        showsMockRepository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        var handler = new UpdateShowPriceCommandHandler(showsMockRepository.Object);

        var command = new UpdateShowPriceCommand() { Id = show.Id, NewAmount = updatedShowPrice };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        showsMockRepository.Verify(x => x.Update(It.Is<Show>(x => x.Id == show.Id)), Times.Once());
    }
}

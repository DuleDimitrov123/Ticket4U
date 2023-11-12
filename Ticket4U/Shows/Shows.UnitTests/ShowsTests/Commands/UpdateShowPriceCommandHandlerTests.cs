using AutoFixture.Xunit2;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shows.Application.Features.Shows.Commands.UpdateShowPrice;
using Shows.Domain.Shows;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.ShowsTests.Commands;

public class UpdateShowPriceCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task UpdateShowPrice([Frozen] Mock<IQueryRepository<Show>> mockQueryRepository,
        [Frozen] Mock<ICommandRepository<Show>> mockCommandRepository,
        UpdateShowPriceCommandHandler handler)
    {
        //arrange
        var initialShowPrice = 50;
        var updatedShowPrice = 150;

        var show = Show.Create("ShowName", "ShowDescription", "ShowPictureBase64", "ShowLocation", NumberOfPlaces.Create(100),
            Money.Create("rsd", initialShowPrice), DateTime.Now.AddDays(10), Guid.NewGuid(), Guid.NewGuid());
        var showPropertyInfo = typeof(Show).GetProperty("Id");
        showPropertyInfo!.SetValue(show, Guid.NewGuid());

        mockQueryRepository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        handler = new UpdateShowPriceCommandHandler(mockQueryRepository.Object, mockCommandRepository.Object);

        var command = new UpdateShowPriceCommand() { Id = show.Id, NewAmount = updatedShowPrice };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        mockCommandRepository.Verify(x => x.Update(It.Is<Show>(x => x.Id == show.Id)), Times.Once());
    }
}

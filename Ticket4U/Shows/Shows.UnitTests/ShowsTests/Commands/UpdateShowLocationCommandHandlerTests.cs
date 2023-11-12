using AutoFixture.Xunit2;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shows.Application.Features.Shows.Commands.UpdateShowLocation;
using Shows.Domain.Shows;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.ShowsTests.Commands;

public class UpdateShowLocationCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task UpdateShowLocation([Frozen] Mock<IQueryRepository<Show>> mockQueryRepository,
        [Frozen] Mock<ICommandRepository<Show>> mockCommandRepository,
        UpdateShowLocationCommandHandler handler)
    {
        //arrange
        var initialShowLocation = "ShowLocation";
        var updatedShowLocation = "UpdatedShowLocation";

        var show = Show.Create("ShowName", "ShowDescription", "ShowPictureBase64", initialShowLocation, NumberOfPlaces.Create(100),
            Money.Create("rsd", 100), DateTime.Now.AddDays(10), Guid.NewGuid(), Guid.NewGuid());
        var showPropertyInfo = typeof(Show).GetProperty("Id");
        showPropertyInfo!.SetValue(show, Guid.NewGuid());

        mockQueryRepository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        handler = new UpdateShowLocationCommandHandler(mockQueryRepository.Object, mockCommandRepository.Object);

        var command = new UpdateShowLocationCommand() { Id = show.Id, NewLocation = updatedShowLocation };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        mockCommandRepository.Verify(x => x.Update(It.Is<Show>(x => x.Id == show.Id)), Times.Once());
    }
}

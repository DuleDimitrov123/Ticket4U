using AutoFixture.Xunit2;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shows.Application.Features.Shows.Commands.UpdateShowName;
using Shows.Domain.Shows;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.ShowsTests.Commands;

public class UpdateShowNameCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task UpdateShowName([Frozen] Mock<IQueryRepository<Show>> mockQueryRepository,
        [Frozen] Mock<ICommandRepository<Show>> mockCommandRepository,
        UpdateShowNameCommandHandler handler)
    {
        //arrange
        var initialShowName = "ShowName";
        var updatedShowName = "UpdatedShowName";

        var show = Show.Create(initialShowName, "ShowDescription", "ShowPictureBase64", "ShowLocation", NumberOfPlaces.Create(100),
            Money.Create("rsd", 100), DateTime.Now.AddDays(10), Guid.NewGuid(), Guid.NewGuid());
        var showPropertyInfo = typeof(Show).GetProperty("Id");
        showPropertyInfo!.SetValue(show, Guid.NewGuid());

        mockQueryRepository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(show);

        handler = new UpdateShowNameCommandHandler(mockQueryRepository.Object, mockCommandRepository.Object);

        var command = new UpdateShowNameCommand() { Id = show.Id, NewName = updatedShowName };

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        mockCommandRepository.Verify(x => x.Update(It.Is<Show>(x => x.Id == show.Id)), Times.Once());
    }
}

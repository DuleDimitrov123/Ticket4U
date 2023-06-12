using Moq;
using Shouldly;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Commands.CreateShow;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;
using Shared.Domain;
using MediatR;
using Shows.Application.Features.Shows.Notifications.ShowCreated;

namespace Shows.UnitTests.ShowsTests.Commands;

public class CreateShowCommandHandlerTests : QueryCommandHandlerTestBase
{
    [Fact]
    public async Task CreateNewShow()
    {
        //arrange
        var performer = Performer.Create("Performer1");

        var performerPropertyInfo = typeof(Performer).GetProperty("Id");
        performerPropertyInfo!.SetValue(performer, Guid.NewGuid());

        var category = Category.Create("Category1", "Description of Category1");

        var categoryPropertyInfo = typeof(Category).GetProperty("Id");
        categoryPropertyInfo!.SetValue(category, Guid.NewGuid());

        var shows = new List<Show>();

        var showsMockRepository = new Mock<IRepository<Show>>();
        showsMockRepository.Setup(repo => repo.Add(It.IsAny<Show>()))
            .ReturnsAsync(
                (Show show) =>
                {
                    var showPropertyInfo = typeof(Show).GetProperty("Id");
                    showPropertyInfo!.SetValue(show, Guid.NewGuid());
                    shows.Add(show);
                    return show;
                });

        var performerMockRepository = new Mock<IRepository<Performer>>();
        performerMockRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(performer);

        var categoryMockRepository = new Mock<IRepository<Category>>();
        categoryMockRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(category);

        var mediatorMock = new Mock<IMediator>();

        var command = new CreateShowCommand()
        {
            Name = "ShowName",
            Location = "ShowLocation",
            NumberOfplaces = 100,
            TicketPriceCurrency = "rsd",
            TickerPriceAmount = 1000,
            StartingDateTime = DateTime.Now.AddDays(10),
            PerformerId = performer.Id,
            CategoryId = category.Id
        };

        var handler = new CreateShowCommandHandler(showsMockRepository.Object, categoryMockRepository.Object, performerMockRepository.Object, mediatorMock.Object);

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        result.ShouldNotBe(Guid.Empty);
    }
}

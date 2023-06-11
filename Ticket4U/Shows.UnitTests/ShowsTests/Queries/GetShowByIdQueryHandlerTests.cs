using Moq;
using Shouldly;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Queries.GetShowById;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;

namespace Shows.UnitTests.ShowsTests.Queries;

public class GetShowByIdQueryHandlerTests : QueryCommandHandlerTestBase
{
    [Fact]
    public async Task GetShowById()
    {
        //arrange
        var performer = Performer.Create("Performer1");

        var performerPropertyInfo = typeof(Performer).GetProperty("Id");
        performerPropertyInfo!.SetValue(performer, Guid.NewGuid());

        var category = Category.Create("Category1", "Description of Category1");

        var categoryPropertyInfo = typeof(Category).GetProperty("Id");
        categoryPropertyInfo!.SetValue(category, Guid.NewGuid());

        var show = Show.Create("ShowName", "ShowLocation", NumberOfPlaces.Create(100),
            Money.Create("rsd", 100), DateTime.Now.AddDays(10), performer.Id, category.Id);

        var showsMockRepository = new Mock<IRepository<Show>>();
        showsMockRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(show);

        var handler = new GetShowByIdQueryHandler(_mapper, showsMockRepository.Object);

        //act
        var result = await handler.Handle(new GetShowByIdQuery() { ShowId = Guid.NewGuid()}, CancellationToken.None);

        //assert
        result.Name.ShouldBe(show.Name);
        result.Location.ShouldBe(show.Location);
        result.NumberOfplaces.ShouldBe(show.NumberOfPlaces.Value);
        result.TicketPriceCurrency.ShouldBe(show.TicketPrice.Currency);
        result.TickerPriceAmount.ShouldBe(show.TicketPrice.Amount);
    }
}

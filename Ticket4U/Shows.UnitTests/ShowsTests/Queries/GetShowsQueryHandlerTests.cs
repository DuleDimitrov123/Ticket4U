using Moq;
using Shouldly;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Queries.GetShows;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;

namespace Shows.UnitTests.ShowsTests.Queries;

public class GetShowsQueryHandlerTests : QueryCommandHandlerTestBase
{
    [Fact]
    public async Task GetAllShows()
    {
        //arrange
        var performer = Performer.Create("Performer1");

        var performerPropertyInfo = typeof(Performer).GetProperty("Id");
        performerPropertyInfo!.SetValue(performer, Guid.NewGuid());

        var category = Category.Create("Category1", "Description of Category1");

        var categoryPropertyInfo = typeof(Category).GetProperty("Id");
        categoryPropertyInfo!.SetValue(category, Guid.NewGuid());

        var shows = new List<Show>()
        {
            Show.Create("ShowName", "ShowLocation", NumberOfPlaces.Create(100), Money.Create("rsd", 100), DateTime.Now.AddDays(10), performer.Id, category.Id)
        };

        var showsMockRepository = new Mock<IRepository<Show>>();
        showsMockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(shows);

        var handler = new GetShowsQueryHandler(_mapper, showsMockRepository.Object);

        //act
        var result = await handler.Handle(new GetShowsQuery(), CancellationToken.None);

        //assert
        foreach (var item in result)
        {
            shows.Select(s => s.Name).ShouldContain(item.Name);
            shows.Select(s => s.Location).ShouldContain(item.Location);
            shows.Select(s => s.NumberOfPlaces.Value).ShouldContain(item.NumberOfplaces);
            shows.Select(s => s.TicketPrice.Currency).ShouldContain(item.TicketPriceCurrency);
            shows.Select(s => s.TicketPrice.Amount).ShouldContain(item.TickerPriceAmount);
        }
    }
}

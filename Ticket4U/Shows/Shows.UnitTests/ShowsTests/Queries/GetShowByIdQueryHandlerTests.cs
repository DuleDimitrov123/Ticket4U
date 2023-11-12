using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shouldly;
using Shows.Application.Features.Shows.Queries.GetShowById;
using Shows.Application.Profiles;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.ShowsTests.Queries;

public class GetShowByIdQueryHandlerTests
{
    private readonly IMapper _mapper;

    public GetShowByIdQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Theory]
    [AutoMoqData]
    public async Task GetShowById([Frozen] Mock<IQueryRepository<Show>> mockQueryRepository,
        GetShowByIdQueryHandler handler)
    {
        //arrange
        var performer = Performer.Create("Performer1");

        var performerPropertyInfo = typeof(Performer).GetProperty("Id");
        performerPropertyInfo!.SetValue(performer, Guid.NewGuid());

        var category = Category.Create("Category1", "Description of Category1");

        var categoryPropertyInfo = typeof(Category).GetProperty("Id");
        categoryPropertyInfo!.SetValue(category, Guid.NewGuid());

        var show = Show.Create("ShowName", "ShowDescription", "ShowPictureBase64", "ShowLocation", NumberOfPlaces.Create(100),
            Money.Create("rsd", 100), DateTime.Now.AddDays(10), performer.Id, category.Id);

        mockQueryRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(show);

        handler = new GetShowByIdQueryHandler(_mapper, mockQueryRepository.Object);

        //act
        var result = await handler.Handle(new GetShowByIdQuery() { ShowId = Guid.NewGuid() }, CancellationToken.None);

        //assert
        result.Name.ShouldBe(show.Name);
        result.Location.ShouldBe(show.Location);
        result.NumberOfplaces.ShouldBe(show.NumberOfPlaces.Value);
        result.TicketPriceCurrency.ShouldBe(show.TicketPrice.Currency);
        result.TickerPriceAmount.ShouldBe(show.TicketPrice.Amount);
    }
}

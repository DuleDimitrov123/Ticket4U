using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Shouldly;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Shows.Queries.GetShowDetailById;
using Shows.Application.Profiles;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.ShowsTests.Queries;

public class GetShowDetailByIdQueryHandlerTests
{
    private readonly IMapper _mapper;

    public GetShowDetailByIdQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Theory]
    [AutoMoqData]
    public async Task GetShowDetailById([Frozen] Mock<IShowQueryRepository> mockShowQueryRepository,
        GetShowDetailByIdQueryHandler handler)
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

        //needed Id because it is passed when creating show message
        var showPropertyInfo = typeof(Show).GetProperty("Id");
        showPropertyInfo!.SetValue(show, Guid.NewGuid());

        show.AddShowMessage("ShowMessage1Name", "ShowMessage1Value");

        mockShowQueryRepository.Setup(s => s.GetShowWithShowMessages(It.IsAny<Guid>()))
            .ReturnsAsync(show);

        handler = new GetShowDetailByIdQueryHandler(_mapper, mockShowQueryRepository.Object);

        //act
        var result = await handler.Handle(new GetShowDetailByIdQuery() { ShowId = Guid.NewGuid() }, CancellationToken.None);

        //assert
        result.Name.ShouldBe(show.Name);
        result.Location.ShouldBe(show.Location);
        result.StartingDateTime.ShouldBe(show.StartingDateTime);
        result.NumberOfplaces.ShouldBe(show.NumberOfPlaces.Value);
        result.TicketPriceCurrency.ShouldBe(show.TicketPrice.Currency);
        result.TickerPriceAmount.ShouldBe(show.TicketPrice.Amount);

        foreach (var showMessage in result.ShowMessages)
        {
            show.ShowMessages.Select(sm => sm.Name).ShouldContain(showMessage.Name);
            show.ShowMessages.Select(sm => sm.Value).ShouldContain(showMessage.Value);
        }
    }
}

using AutoFixture.Xunit2;
using AutoMapper;
using MediatR;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shouldly;
using Shows.Application.Features.Shows.Commands.CreateShow;
using Shows.Application.Profiles;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.ShowsTests.Commands;

public class CreateShowCommandHandlerTests
{
    private readonly IMapper _mapper;

    public CreateShowCommandHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Theory]
    [AutoMoqData]
    public async Task CreateNewShow([Frozen] Mock<ICommandRepository<Show>> mockShowCommandRepository,
        [Frozen] Mock<IQueryRepository<Category>> mockCategoryQueryRepository,
        [Frozen] Mock<IQueryRepository<Performer>> mockPerformerQueryRepository,
        [Frozen] Mock<IMediator> mockMediator,
        CreateShowCommandHandler handler)
    {
        //arrange
        var performer = Performer.Create("Performer1");

        var performerPropertyInfo = typeof(Performer).GetProperty("Id");
        performerPropertyInfo!.SetValue(performer, Guid.NewGuid());

        var category = Category.Create("Category1", "Description of Category1");

        var categoryPropertyInfo = typeof(Category).GetProperty("Id");
        categoryPropertyInfo!.SetValue(category, Guid.NewGuid());

        var shows = new List<Show>();

        mockShowCommandRepository.Setup(repo => repo.Add(It.IsAny<Show>()))
            .ReturnsAsync(
                (Show show) =>
                {
                    var showPropertyInfo = typeof(Show).GetProperty("Id");
                    showPropertyInfo!.SetValue(show, Guid.NewGuid());
                    shows.Add(show);
                    return show;
                });

        mockPerformerQueryRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(performer);

        mockCategoryQueryRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(category);

        var command = new CreateShowCommand()
        {
            Name = "ShowName",
            Location = "ShowLocation",
            Description = "ShowDescription",
            Picture = "DefaultPictureBase64",
            NumberOfplaces = 100,
            TicketPriceCurrency = "rsd",
            TickerPriceAmount = 1000,
            StartingDateTime = DateTime.Now.AddDays(10),
            PerformerId = performer.Id,
            CategoryId = category.Id
        };

        handler = new CreateShowCommandHandler(mockShowCommandRepository.Object, mockCategoryQueryRepository.Object, mockPerformerQueryRepository.Object, mockMediator.Object);

        //act
        var result = await handler.Handle(command, CancellationToken.None);

        //assert
        result.ShouldNotBe(Guid.Empty);
    }
}

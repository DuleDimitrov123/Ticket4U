using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shouldly;
using Shows.Application.Features.Performers.Queries;
using Shows.Application.Features.Performers.Queries.GetPerformers;
using Shows.Application.Profiles;
using Shows.Domain.Performers;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.Performers.Queries;

public class GetPerformersQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly IList<Performer> _performers;

    public GetPerformersQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
        _performers = new List<Performer>()
        {
            Performer.Create("Performer1", new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName1", "PerformerInfoValue1")
            }),
            Performer.Create("Performer2", new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName2", "PerformerInfoValue2")
            }),
            Performer.Create("Performer3", new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName3", "PerformerInfoValue3")
            })
        };
    }

    [Theory]
    [AutoMoqData]
    public async Task GetPerformersListTest([Frozen] Mock<IQueryRepository<Performer>> mockPerformerQueryRepository,
        GetPerformersQueryHandler handler)
    {
        mockPerformerQueryRepository.Setup(repo => repo.GetAll()).ReturnsAsync(_performers);

        handler = new GetPerformersQueryHandler(_mapper, mockPerformerQueryRepository.Object);

        var result = await handler.Handle(new GetPerformersQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<PerformerResponse>>();

        result.Count.ShouldBe(_performers.Count);
    }
}

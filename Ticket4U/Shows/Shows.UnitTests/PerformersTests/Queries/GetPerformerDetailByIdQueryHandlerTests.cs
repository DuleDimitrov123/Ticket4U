using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Shouldly;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Performers.Queries.GetPerformerDetailById;
using Shows.Application.Profiles;
using Shows.Domain.Performers;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.Performers.Queries;

public class GetPerformerDetailByIdQueryHandlerTests
{
    private readonly IMapper _mapper;

    public GetPerformerDetailByIdQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Theory]
    [AutoMoqData]
    public async Task GetPerformerDetailByIdTest([Frozen] Mock<IPerformerQueryRepository> mockPerformerQueryRepository,
        GetPerformerDetailByIdQueryHandler handler)
    {
        var performer = Performer.Create("Performer1",
            new List<PerformerInfo>()
            {
                PerformerInfo.Create("PerformerInfoName1", "PerformerInfoValue1")
            });

        mockPerformerQueryRepository.Setup(repo => repo.GetPerformerWithPerformerInfos(It.IsAny<Guid>())).ReturnsAsync(performer);

        handler = new GetPerformerDetailByIdQueryHandler(_mapper, mockPerformerQueryRepository.Object);

        var result = await handler.Handle(
            new GetPerformerDetailByIdQuery()
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None);

        result.ShouldNotBeNull();

        result.Name.ShouldBe(performer.Name);

        result.PerformerInfos.Select(pi => pi.Name)
            .ShouldContain(performer.PerformerInfos.Select(pi2 => pi2.Name).FirstOrDefault());

        result.PerformerInfos.Select(pi => pi.Value)
            .ShouldContain(performer.PerformerInfos.Select(pi2 => pi2.Value).FirstOrDefault());
    }
}

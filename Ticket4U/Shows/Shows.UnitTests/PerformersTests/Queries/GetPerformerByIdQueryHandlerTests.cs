using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shouldly;
using Shows.Application.Features.Performers.Queries.GetPerformerById;
using Shows.Application.Profiles;
using Shows.Domain.Performers;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.Performers.Queries;

public class GetPerformerByIdQueryHandlerTests
{
    private readonly IMapper _mapper;

    public GetPerformerByIdQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Theory]
    [AutoMoqData]
    public async Task GetPerformerByIdTest([Frozen] Mock<IQueryRepository<Performer>> queryMockRepository,
        GetPerformerByIdQueryHandler handler)
    {
        var performer = Performer.Create("Performer1");

        queryMockRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(performer);
        handler = new GetPerformerByIdQueryHandler(_mapper, queryMockRepository.Object);

        var result = await handler.Handle(
            new GetPerformerByIdQuery()
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None);

        result.ShouldNotBeNull();

        result.Name.ShouldBe(performer.Name);
    }
}

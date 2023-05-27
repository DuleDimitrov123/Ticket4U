using Moq;
using Shouldly;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Features.Performers.Queries.GetPerformerById;
using Shows.Application.Features.Performers.Queries.GetPerformerDetailById;
using Shows.UnitTests.Mocks;

namespace Shows.UnitTests.Performers.Queries;

public class GetPerformerDetailByIdQueryHandlerTests : QueryCommandHandlerTestBase
{
    private IMock<IPerformerRepository> _mockSpecificPerformerRepository;

    public GetPerformerDetailByIdQueryHandlerTests()
        : base()
    {
        _mockSpecificPerformerRepository = RepositoryMocks.InitSpecificMockPerformerRepository();
    }

    [Fact]
    public async Task GetPerformerDetailByIdTest()
    {
        var handler = new GetPerformerDetailByIdQueryHandler(_mapper, _mockSpecificPerformerRepository.Object);

        var result = await handler.Handle(
            new GetPerformerDetailByIdQuery()
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None);

        result.ShouldNotBeNull();

        result.Name.ShouldBe(Shows.UnitTests.Dummies.Performers.Performer1.Name);


        result.PerformerInfos.Select(pi => pi.Name)
            .ShouldContain(Shows.UnitTests.Dummies.Performers.Performer1.PerformerInfos.Select(pi2 => pi2.Name).FirstOrDefault());

        result.PerformerInfos.Select(pi => pi.Value)
            .ShouldContain(Shows.UnitTests.Dummies.Performers.Performer1.PerformerInfos.Select(pi2 => pi2.Value).FirstOrDefault());
    }
}

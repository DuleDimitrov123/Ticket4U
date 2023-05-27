using Moq;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Performers;
using Shows.UnitTests.Mocks;

namespace Shows.UnitTests.Performers;

public class PerformersQueryCommandHandlerTestBase : QueryCommandHandlerTestBase
{
    protected IMock<IRepository<Performer>> _mockPerformerRepository;

    public PerformersQueryCommandHandlerTestBase()
        : base()
    {
        _mockPerformerRepository = RepositoryMocks.InitPerformerMockRepository();
    }
}

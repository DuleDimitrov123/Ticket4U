using Moq;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Categories;
using Shows.UnitTests.Mocks;

namespace Shows.UnitTests.Categories;

public class CategoriesQueryCommandHandlerTestBase : QueryCommandHandlerTestBase
{
    protected IMock<IRepository<Category>> _mockCategoryRepository;

    public CategoriesQueryCommandHandlerTestBase()
        : base()
    {
        _mockCategoryRepository = RepositoryMocks.InitCategoryMockRepository();
    }
}

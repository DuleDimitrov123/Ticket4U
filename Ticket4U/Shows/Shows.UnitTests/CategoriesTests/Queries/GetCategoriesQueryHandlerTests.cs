using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shouldly;
using Shows.Application.Features.Categories.Queries;
using Shows.Application.Features.Categories.Queries.GetCategories;
using Shows.Application.Profiles;
using Shows.Domain.Categories;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.Categories.Queries;

public class GetCategoriesQueryHandlerTests
{
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Theory]
    [AutoMoqData]
    public async Task GetCategoriesListTest([Frozen] Mock<IQueryRepository<Category>> mockQueryRepository,
        GetCategoriesQueryHandler handler)
    {
        var categories = new List<Category>()
        {
            Category.Create("Category1", "Description of category 1"),
            Category.Create("Category2", "Description of category 2"),
            Category.Create("Category3", "Description of category 3")
        };

        mockQueryRepository.Setup(repo => repo.GetAll()).ReturnsAsync(categories);

        handler = new GetCategoriesQueryHandler(_mapper, mockQueryRepository.Object);

        var result = await handler.Handle(new GetCategoriesQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<CategoryResponse>>();

        result.Count.ShouldBe(categories.Count);
    }
}

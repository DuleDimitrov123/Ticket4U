using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using Shared.Application.Contracts.Persistence;
using Shouldly;
using Shows.Application.Features.Categories.Queries.GetCategoryById;
using Shows.Application.Profiles;
using Shows.Domain.Categories;
using Shows.UnitTests.Helpers;

namespace Shows.UnitTests.Categories.Queries;

public class GetCategoryByIdQueryHandlerTests
{
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Theory]
    [AutoMoqData]
    public async Task GetCategoryByIdTest([Frozen] Mock<IQueryRepository<Category>> mockQueryRepository,
        GetCategoryByIdQueryHandler handler)
    {
        var category = Category.Create("Category1", "Description of category 1");
        mockQueryRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(category);

        handler = new GetCategoryByIdQueryHandler(_mapper, mockQueryRepository.Object);

        var result = await handler.Handle(
            new GetCategoryByIdQuery()
            {
                CategoryId = Guid.Empty
            },
            CancellationToken.None);

        result.ShouldNotBeNull();

        result.Name.ShouldBe(category.Name);
        result.Description.ShouldBe(category.Description);
    }
}

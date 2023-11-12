using AutoMapper;
using MediatR;
using Shared.Application.Contracts.Persistence;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IList<CategoryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Category> _queryRepository;

    public GetCategoriesQueryHandler(IMapper mapper, IQueryRepository<Category> queryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
    }

    public async Task<IList<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _queryRepository.GetAll();

        return _mapper.Map<IList<CategoryResponse>>(categories);
    }
}

using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Queries.GetCategories;

internal class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IList<CategoryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Category> _repository;

    public GetCategoriesQueryHandler(IMapper mapper, IRepository<Category> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IList<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAll();

        return _mapper.Map<IList<CategoryResponse>>(categories);
    }
}

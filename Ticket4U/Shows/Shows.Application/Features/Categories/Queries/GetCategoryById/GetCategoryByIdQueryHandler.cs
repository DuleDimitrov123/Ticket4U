using AutoMapper;
using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Category> _queryRepository;

    public GetCategoryByIdQueryHandler(IMapper mapper, IQueryRepository<Category> queryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
    }

    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _queryRepository.GetById(request.CategoryId);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        return _mapper.Map<CategoryResponse>(category);
    }
}

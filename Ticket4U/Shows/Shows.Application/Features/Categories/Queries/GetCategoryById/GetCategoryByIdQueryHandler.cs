using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shared.Application.Exceptions;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Category> _repository;

    public GetCategoryByIdQueryHandler(IMapper mapper, IRepository<Category> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetById(request.CategoryId);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        return _mapper.Map<CategoryResponse>(category);
    }
}

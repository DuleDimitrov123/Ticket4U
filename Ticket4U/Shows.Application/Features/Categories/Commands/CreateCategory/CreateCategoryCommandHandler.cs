using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Category> _repository;

    public CreateCategoryCommandHandler(IMapper mapper, IRepository<Category> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name, request.Description);

        category = await _repository.Add(category);

        return category.Id;
    }
}

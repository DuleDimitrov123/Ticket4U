using AutoMapper;
using MediatR;
using Shared.Application.Contracts.Persistence;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly ICommandRepository<Category> _commandRepository;

    public CreateCategoryCommandHandler(IMapper mapper, ICommandRepository<Category> commandRepository)
    {
        _mapper = mapper;
        _commandRepository = commandRepository;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name, request.Description);

        category = await _commandRepository.Add(category);

        return category.Id;
    }
}

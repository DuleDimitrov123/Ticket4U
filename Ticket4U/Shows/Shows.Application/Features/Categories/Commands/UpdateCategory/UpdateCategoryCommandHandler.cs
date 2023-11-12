using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly ICommandRepository<Category> _commandRepository;
    private readonly IQueryRepository<Category> _queryRepository;

    public UpdateCategoryCommandHandler(ICommandRepository<Category> commandRepository, IQueryRepository<Category> queryRepository)
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _queryRepository.GetById(request.CategoryId);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        if (!string.IsNullOrEmpty(request.NewName))
        {
            category.UpdateCategoryName(request.NewName);
        }

        if (!string.IsNullOrEmpty(request.NewDescription))
        {
            category.UpdateCategoryDescription(request.NewDescription);
        }

        await _commandRepository.Update(category);

        return Unit.Value;
    }
}

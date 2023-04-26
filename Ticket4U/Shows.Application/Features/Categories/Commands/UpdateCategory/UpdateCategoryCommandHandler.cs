using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IRepository<Category> _repository;

    public UpdateCategoryCommandHandler(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetById(request.CategoryId);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        if (!string.IsNullOrEmpty(request.NewName))
        {
            category.UpdateCategoryName(request.NewName);
        }

        if(!string.IsNullOrEmpty(request.NewDescription))
        {
            category.UpdateCategoryDescription(request.NewDescription);
        }

        await _repository.Update(category);

        return Unit.Value;
    }
}

using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Commands.ArchiveCategory;

public class ArchiveCategoryCommandHandler : IRequestHandler<ArchiveCategoryCommand, Unit>
{
    private readonly IRepository<Category> _repository;

    public ArchiveCategoryCommandHandler(IRepository<Category> repository)
    {
        _repository= repository;
    }

    public async Task<Unit> Handle(ArchiveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetById(request.CategoryId);

        if(category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        category.ArchiveCategory();
        await _repository.Update(category);

        return Unit.Value;
    }
}

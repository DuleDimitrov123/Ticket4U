using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Domain.Categories;

namespace Shows.Application.Features.Categories.Commands.ArchiveCategory;

public class ArchiveCategoryCommandHandler : IRequestHandler<ArchiveCategoryCommand, Unit>
{
    private readonly ICommandRepository<Category> _commandRepository;
    private readonly IQueryRepository<Category> _queryRepository;

    public ArchiveCategoryCommandHandler(ICommandRepository<Category> commandRepository, IQueryRepository<Category> queryRepository)
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
    }

    public async Task<Unit> Handle(ArchiveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _queryRepository.GetById(request.CategoryId);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        category.ArchiveCategory();
        await _commandRepository.Update(category);

        return Unit.Value;
    }
}

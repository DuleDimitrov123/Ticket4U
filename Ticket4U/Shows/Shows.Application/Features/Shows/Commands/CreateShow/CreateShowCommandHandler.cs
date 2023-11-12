using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Application.Features.Shows.Notifications.ShowCreated;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.CreateShow;

public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, Guid>
{
    private readonly ICommandRepository<Show> _showCommandRepository;
    private readonly IQueryRepository<Category> _categoryQueryRepository;
    private readonly IQueryRepository<Performer> _performerQueryRepository;
    private readonly IMediator _mediator;

    public CreateShowCommandHandler(ICommandRepository<Show> showCommandRepository,
        IQueryRepository<Category> categoryQueryRepository,
        IQueryRepository<Performer> performerQueryRepository,
        IMediator mediator)
    {
        _showCommandRepository = showCommandRepository;
        _categoryQueryRepository = categoryQueryRepository;
        _performerQueryRepository = performerQueryRepository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateShowCommand request, CancellationToken cancellationToken)
    {
        var show = request.ToShow();

        var performer = await _performerQueryRepository.GetById(request.PerformerId);

        if (performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.PerformerId);
        }

        var category = await _categoryQueryRepository.GetById(request.CategoryId);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }

        show = await _showCommandRepository.Add(show);

        await _mediator.Publish(new ShowCreatedNotification(show));

        return show.Id;
    }
}

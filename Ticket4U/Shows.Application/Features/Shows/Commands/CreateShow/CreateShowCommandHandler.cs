using MediatR;
using Shows.Application.Contracts.Persistance;
using Shared.Application.Exceptions;
using Shows.Application.Features.Shows.Notifications.ShowCreated;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.CreateShow;

public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, Guid>
{
    private readonly IRepository<Show> _showRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Performer> _performerRepository;
    private readonly IMediator _mediator;

    public CreateShowCommandHandler(IRepository<Show> showRepository, 
        IRepository<Category> categoryRepository, 
        IRepository<Performer> performerRepository,
        IMediator mediator)
    {
        _showRepository = showRepository;
        _categoryRepository = categoryRepository;
        _performerRepository = performerRepository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateShowCommand request, CancellationToken cancellationToken)
    {
        var show = request.ToShow();

        var performer = await _performerRepository.GetById(request.PerformerId);

        if(performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.PerformerId);
        }

        var category = await _categoryRepository.GetById(request.CategoryId);

        if(category == null)
        {
            throw new NotFoundException(nameof(Category), request.CategoryId);
        }
        
        show = await _showRepository.Add(show);

        await _mediator.Publish(new ShowCreatedNotification(show));

        return show.Id;
    }
}

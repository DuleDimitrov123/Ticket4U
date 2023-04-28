using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.CreateShow;

public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, Guid>
{
    private readonly IRepository<Show> _showRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Performer> _performerRepository;

    public CreateShowCommandHandler(IRepository<Show> showRepository, IRepository<Category> categoryRepository, IRepository<Performer> performerRepository)
    {
        _showRepository = showRepository;
        _categoryRepository = categoryRepository;
        _performerRepository = performerRepository;
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

        return show.Id;
    }
}

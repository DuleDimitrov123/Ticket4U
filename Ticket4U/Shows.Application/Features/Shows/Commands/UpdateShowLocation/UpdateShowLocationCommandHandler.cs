using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.UpdateShowLocation;

public class UpdateShowLocationCommandHandler : IRequestHandler<UpdateShowLocationCommand, Unit>
{
    private readonly IRepository<Show> _repository;

    public UpdateShowLocationCommandHandler(IRepository<Show> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateShowLocationCommand request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetById(request.Id);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.Id);
        }

        show.UpdateShowLocation(request.NewLocation);
        await _repository.Update(show);

        return Unit.Value;
    }
}

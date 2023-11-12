using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.UpdateShowLocation;

public class UpdateShowLocationCommandHandler : IRequestHandler<UpdateShowLocationCommand, Unit>
{
    private readonly IQueryRepository<Show> _queryRepository;
    private readonly ICommandRepository<Show> _commandRepository;

    public UpdateShowLocationCommandHandler(IQueryRepository<Show> queryRepository, ICommandRepository<Show> commandRepository)
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
    }

    public async Task<Unit> Handle(UpdateShowLocationCommand request, CancellationToken cancellationToken)
    {
        var show = await _queryRepository.GetById(request.Id);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.Id);
        }

        show.UpdateShowLocation(request.NewLocation);
        await _commandRepository.Update(show);

        return Unit.Value;
    }
}

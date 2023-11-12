using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.UpdateShowName;

public class UpdateShowNameCommandHandler : IRequestHandler<UpdateShowNameCommand, Unit>
{
    private readonly IQueryRepository<Show> _queryRepository;
    private readonly ICommandRepository<Show> _commandRepository;

    public UpdateShowNameCommandHandler(IQueryRepository<Show> queryRepository, ICommandRepository<Show> commandRepository)
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
    }

    public async Task<Unit> Handle(UpdateShowNameCommand request, CancellationToken cancellationToken)
    {
        var show = await _queryRepository.GetById(request.Id);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.Id);
        }

        show.UpdateShowName(request.NewName);
        await _commandRepository.Update(show);

        return Unit.Value;
    }
}

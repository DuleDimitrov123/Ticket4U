using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.DeleteShow;

public class DeleteShowCommandHandler : IRequestHandler<DeleteShowCommand, Unit>
{
    private readonly IQueryRepository<Show> _queryRepository;
    private readonly ICommandRepository<Show> _commandRepository;

    public DeleteShowCommandHandler(IQueryRepository<Show> queryRepository, ICommandRepository<Show> commandRepository)
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
    }

    public async Task<Unit> Handle(DeleteShowCommand request, CancellationToken cancellationToken)
    {
        var show = await _queryRepository.GetById(request.Id);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.Id);
        }

        await _commandRepository.Delete(show);

        return Unit.Value;
    }
}

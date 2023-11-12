using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.AddShowMessage;

public class AddShowMessageCommandHandler : IRequestHandler<AddShowMessageCommand, Unit>
{
    private readonly IShowQueryRepository _showQueryRepository;
    private readonly ICommandRepository<Show> _commandRepository;

    public AddShowMessageCommandHandler(IShowQueryRepository showRepository, ICommandRepository<Show> commandRepository)
    {
        _showQueryRepository = showRepository;
        _commandRepository = commandRepository;
    }

    public async Task<Unit> Handle(AddShowMessageCommand request, CancellationToken cancellationToken)
    {
        var show = await _showQueryRepository.GetShowWithShowMessages(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        show.AddShowMessage(request.ShowMessageName, request.ShowMessageValue);
        await _commandRepository.Update(show);

        return Unit.Value;
    }
}

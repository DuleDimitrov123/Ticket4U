using MediatR;
using Reservations.Domain.Shows;
using Shared.Application.Contracts.Persistence;

namespace Reservations.Application.Features.Shows.Commands.CreateShow;

public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, Guid>
{
    private readonly ICommandRepository<Show> _commandRepository;

    public CreateShowCommandHandler(ICommandRepository<Show> commandRepository)
    {
        _commandRepository = commandRepository;
    }

    public async Task<Guid> Handle(CreateShowCommand request, CancellationToken cancellationToken)
    {
        var show = Show.Create(request.Name, request.StartingDateTime, request.NumberOfPlaces, request.ExternalId);

        show = await _commandRepository.Add(show);

        return show.Id;
    }
}

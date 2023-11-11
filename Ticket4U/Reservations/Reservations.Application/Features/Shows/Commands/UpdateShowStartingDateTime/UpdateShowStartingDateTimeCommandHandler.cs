using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Shows;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Shows.Commands.UpdateShowStartingDateTime;

public class UpdateShowStartingDateTimeCommandHandler : IRequestHandler<UpdateShowStartingDateTimeCommand, Unit>
{
    private readonly IShowRepository _repository;

    public UpdateShowStartingDateTimeCommandHandler(IShowRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateShowStartingDateTimeCommand request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetShowByExternalId(request.ExternalShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ExternalShowId);
        }

        show.UpdateStartingDateTime(request.NewStartingDateTime);
        await _repository.Update(show);

        return Unit.Value;
    }
}

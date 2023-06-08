using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.UpdateShowStartingDateTime;

public class UpdateShowStartingDateTimeCommandHandler : IRequestHandler<UpdateShowStartingDateTimeCommand, Unit>
{
    private readonly IShowRepository _repository;

    public UpdateShowStartingDateTimeCommandHandler(IShowRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateShowStartingDateTimeCommand request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetShowWithShowMessages(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        show.UpdateStartingDateTime(request.NewStartingDateTime);
        await _repository.Update(show);

        return Unit.Value;
    }
}

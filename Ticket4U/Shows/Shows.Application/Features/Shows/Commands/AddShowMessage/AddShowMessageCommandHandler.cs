using MediatR;
using Shows.Application.Contracts.Persistance;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.AddShowMessage;

public class AddShowMessageCommandHandler : IRequestHandler<AddShowMessageCommand, Unit>
{
    private readonly IShowRepository _repository;

    public AddShowMessageCommandHandler(IShowRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddShowMessageCommand request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetShowWithShowMessages(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        show.AddShowMessage(request.ShowMessageName, request.ShowMessageValue);
        await _repository.Update(show);

        return Unit.Value;
    }
}

using MediatR;
using Shows.Application.Contracts.Persistance;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.DeleteShow;

public class DeleteShowCommandHandler : IRequestHandler<DeleteShowCommand, Unit>
{
    private readonly IRepository<Show> _repository;

    public DeleteShowCommandHandler(IRepository<Show> repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(DeleteShowCommand request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetById(request.Id);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.Id);
        }

        await _repository.Delete(show);

        return Unit.Value;
    }
}

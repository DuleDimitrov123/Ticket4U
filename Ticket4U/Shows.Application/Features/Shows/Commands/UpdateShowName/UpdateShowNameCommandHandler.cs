using MediatR;
using Shows.Application.Contracts.Persistance;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.UpdateShowName;

public class UpdateShowNameCommandHandler : IRequestHandler<UpdateShowNameCommand, Unit>
{
    private readonly IRepository<Show> _repository;

    public UpdateShowNameCommandHandler(IRepository<Show> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateShowNameCommand request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetById(request.Id);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.Id);
        }

        show.UpdateShowName(request.NewName);
        await _repository.Update(show);

        return Unit.Value;
    }
}

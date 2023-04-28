using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.UpdateShowPrice;

public class UpdateShowPriceCommandHandler : IRequestHandler<UpdateShowPriceCommand, Unit>
{
    private readonly IRepository<Show> _repository;

    public UpdateShowPriceCommandHandler(IRepository<Show> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateShowPriceCommand request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetById(request.Id);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.Id);
        }

        show.UpdateTicketPriceAmount(request.NewAmount);
        await _repository.Update(show);

        return Unit.Value;
    }
}

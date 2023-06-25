using MediatR;
using Shared.Application.Exceptions;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.ChangeShowStatus;

public class ChangeShowStatusCommandHandler : IRequestHandler<ChangeShowStatusCommand, Unit>
{
    private readonly IRepository<Show> _repository;

    public ChangeShowStatusCommandHandler(IRepository<Show> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(ChangeShowStatusCommand request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetById(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        if(request.IsSoldOut)
        {
            if (show.Status == ShowStatus.IsSoldOut)
            {
                throw new Exception("Can't sell out already sold out show!");
            }

            show.SellOutTheShow();
        }
        else
        {
            if (show.Status == ShowStatus.HasTickets)
            {
                throw new Exception("Can't unsell out show that has tickets!");
            }

            show.UnSellOutTheShow();
        }

        await _repository.Update(show);

        return Unit.Value;
    }
}

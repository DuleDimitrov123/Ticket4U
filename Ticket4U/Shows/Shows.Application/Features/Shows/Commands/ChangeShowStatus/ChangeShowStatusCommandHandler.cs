using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.ChangeShowStatus;

public class ChangeShowStatusCommandHandler : IRequestHandler<ChangeShowStatusCommand, Unit>
{
    private readonly IQueryRepository<Show> _queryRepository;
    private readonly ICommandRepository<Show> _commandRepository;

    public ChangeShowStatusCommandHandler(IQueryRepository<Show> queryRepository, ICommandRepository<Show> commandRepository)
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
    }

    public async Task<Unit> Handle(ChangeShowStatusCommand request, CancellationToken cancellationToken)
    {
        var show = await _queryRepository.GetById(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        if (request.IsSoldOut)
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

        await _commandRepository.Update(show);

        return Unit.Value;
    }
}

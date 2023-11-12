using MediatR;
using Reservations.Application.Features.Shows.Notifications.ChangedShowStatus;
using Reservations.Application.Services;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shared.Domain.Events;

namespace Reservations.Application.Features.Reservations.Commands.UpdateNumberOfReservations;

public class UpdateNumberOfReservationsCommandHandler : IRequestHandler<UpdateNumberOfReservationsCommand, Unit>
{
    private readonly ICommandRepository<Reservation> _reservationCommandRepository;
    private readonly IQueryRepository<Reservation> _reservationQueryRepository;
    private readonly ICheckShowReservation _checkShowReservation;
    private readonly IQueryRepository<Show> _showQueryRepository;
    private readonly IMediator _mediator;

    public UpdateNumberOfReservationsCommandHandler(
        ICommandRepository<Reservation> reservationRepository,
        IQueryRepository<Reservation> reservationQueryRepository,
        IQueryRepository<Show> showRepository,
        ICheckShowReservation checkShowReservation,
        IMediator mediator)
    {
        _reservationCommandRepository = reservationRepository;
        _reservationQueryRepository = reservationQueryRepository;
        _checkShowReservation = checkShowReservation;
        _showQueryRepository = showRepository;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(UpdateNumberOfReservationsCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationQueryRepository.GetById(request.Id);

        if (reservation == null)
        {
            throw new NotFoundException(nameof(Reservation), request.Id);
        }

        var show = await _showQueryRepository.GetById(reservation.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), reservation.ShowId);
        }

        var initialAvailableReservations = await _checkShowReservation.GetNumberOfAvailableReservations(show);

        var newAvailableReservations = initialAvailableReservations + reservation.NumberOfReservations.Value - request.NewNumberOfReservations;

        if (newAvailableReservations < 0)
        {
            throw new Exception("Not enough space for this show to reserve!");
        }

        if (newAvailableReservations == 0)
        {
            show.SellOutTheShow();

            await _mediator.Publish(
                new ChangedShowStatusNotification(
                    new ChangedShowStatusEvent()
                    {
                        ShowId = show.ExternalId,
                        IsSoldOut = true
                    }));
        }

        if (initialAvailableReservations == 0 && newAvailableReservations > 0)
        {
            show.UnSellOutTheShow();

            //if initial available reservations were 0, that means the show was sold, and now,
            //new available reservations is not null, show is not sold anymore

            await _mediator.Publish(
                new ChangedShowStatusNotification(
                    new ChangedShowStatusEvent()
                    {
                        ShowId = show.ExternalId,
                        IsSoldOut = false
                    }));
        }

        reservation.UpdateNumberOfReservations(request.NewNumberOfReservations);
        await _reservationCommandRepository.Update(reservation);

        return Unit.Value;
    }
}

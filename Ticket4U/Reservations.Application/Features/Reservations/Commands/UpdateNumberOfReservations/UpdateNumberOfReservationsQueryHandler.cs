using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Shows.Notifications.ChangedShowStatus;
using Reservations.Application.Services;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Shared.Application.Exceptions;
using Shared.Domain.Events;

namespace Reservations.Application.Features.Reservations.Commands.UpdateNumberOfReservations;

public class UpdateNumberOfReservationsQueryHandler : IRequestHandler<UpdateNumberOfReservationsQuery, Unit>
{
    private readonly IRepository<Reservation> _reservationRpository;
    private readonly ICheckShowReservation _checkShowReservation;
    private readonly IRepository<Show> _showRepository;
    private readonly IMediator _mediator;

    public UpdateNumberOfReservationsQueryHandler(
        IRepository<Reservation> reservationRepository,
        IRepository<Show> showRepository,
        ICheckShowReservation checkShowReservation,
        IMediator mediator)
    {
        _reservationRpository = reservationRepository;
        _checkShowReservation = checkShowReservation;
        _showRepository = showRepository;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(UpdateNumberOfReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRpository.GetById(request.Id);

        if (reservation == null)
        {
            throw new NotFoundException(nameof(Reservation), request.Id);
        }

        var show = await _showRepository.GetById(reservation.ShowId);

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
        await _reservationRpository.Update(reservation);

        return Unit.Value;
    }
}

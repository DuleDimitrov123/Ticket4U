using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Shows.Notifications.ChangedShowStatus;
using Reservations.Application.Services;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Shared.Application.Exceptions;
using Shared.Domain.Events;

namespace Reservations.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly IRepository<Show> _showRepository;
    private readonly IRepository<User> _userRepository;
    private readonly ICheckShowReservation _checkShowReservation;
    private readonly IMediator _mediator;

    public CreateReservationCommandHandler(
        IRepository<Reservation> reservationRepository, 
        IRepository<Show> showRepository,
        IRepository<User> userRepository,
        ICheckShowReservation checkShowReservation,
        IMediator mediator)
    {
        _reservationRepository = reservationRepository;
        _showRepository = showRepository;
        _userRepository = userRepository;
        _checkShowReservation = checkShowReservation;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var show = await _showRepository.GetById(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        var user = await _userRepository.GetById(request.UserId);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }

        var availableReservations = await _checkShowReservation.GetNumberOfAvailableReservations(show);

        if (availableReservations < request.NumberOfReservations)
        {
            throw new Exception("Not enough space for this show to reserve!");
        }

        if(availableReservations == request.NumberOfReservations)
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

        var reservation = Reservation.Create(request.UserId, request.ShowId, NumberOfReservations.Create(request.NumberOfReservations));

        reservation = await _reservationRepository.Add(reservation);

        return reservation.Id;
    }
}

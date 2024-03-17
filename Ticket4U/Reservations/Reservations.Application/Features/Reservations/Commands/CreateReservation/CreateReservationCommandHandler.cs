using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Shows.Notifications.ChangedShowStatus;
using Reservations.Application.Services;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shared.Domain.Events;

namespace Reservations.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly ICommandRepository<Reservation> _reservationCommandRepository;
    private readonly IShowQueryRepository _showQueryRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly ICheckShowReservation _checkShowReservation;
    private readonly IMediator _mediator;

    public CreateReservationCommandHandler(
        ICommandRepository<Reservation> reservationRepository,
        IShowQueryRepository showRepository,
        IUserQueryRepository userRepository,
        ICheckShowReservation checkShowReservation,
        IMediator mediator)
    {
        _reservationCommandRepository = reservationRepository;
        _showQueryRepository = showRepository;
        _userQueryRepository = userRepository;
        _checkShowReservation = checkShowReservation;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var show = await _showQueryRepository.GetShowByExternalId(request.ExternalShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ExternalShowId);
        }

        var user = await _userQueryRepository.GetUserByExternalId(request.ExternalUserId);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.ExternalUserId);
        }

        var availableReservations = await _checkShowReservation.GetNumberOfAvailableReservations(show);

        if (availableReservations < request.NumberOfReservations)
        {
            throw new Exception("Not enough space for this show to reserve!");
        }

        if (availableReservations == request.NumberOfReservations)
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

        var reservation = Reservation.Create(user.Id, show.Id, NumberOfReservations.Create(request.NumberOfReservations));

        reservation = await _reservationCommandRepository.Add(reservation);

        return reservation.Id;
    }
}

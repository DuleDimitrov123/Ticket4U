using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly IRepository<Show> _showRepository;
    private readonly IRepository<User> _userRepository;

    public CreateReservationCommandHandler(
        IRepository<Reservation> reservationRepository, 
        IRepository<Show> showRepository,
        IRepository<User> userRepository)
    {
        _reservationRepository = reservationRepository;
        _showRepository = showRepository;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        //TODO: Check if user and show exist
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

        var reservation = Reservation.Create(request.UserId, request.ShowId, NumberOfReservations.Create(request.NumberOfReservations));

        reservation = await _reservationRepository.Add(reservation);

        return reservation.Id;
    }
}

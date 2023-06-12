using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Reservations;

namespace Reservations.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
{
    private readonly IRepository<Reservation> _repository;

    public CreateReservationCommandHandler(IRepository<Reservation> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = Reservation.Create(request.UserId, request.ShowId, NumberOfReservations.Create(request.NumberOfReservations));

        reservation = await _repository.Add(reservation);

        return reservation.Id;
    }
}

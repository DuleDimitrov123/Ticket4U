using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Reservations;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Reservations.Commands.UpdateNumberOfReservations;

public class UpdateNumberOfReservationsQueryHandler : IRequestHandler<UpdateNumberOfReservationsQuery, Unit>
{
    private readonly IRepository<Reservation> _repository;

    public UpdateNumberOfReservationsQueryHandler(IRepository<Reservation> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateNumberOfReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _repository.GetById(request.Id);

        if (reservation == null)
        {
            throw new NotFoundException(nameof(Reservation), request.Id);
        }

        reservation.UpdateNumberOfReservations(request.NewNumberOfReservations);
        await _repository.Update(reservation);

        return Unit.Value;
    }
}

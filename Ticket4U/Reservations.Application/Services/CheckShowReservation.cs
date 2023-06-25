using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Shows;

namespace Reservations.Application.Services;

public interface ICheckShowReservation
{
    Task<int> GetNumberOfAvailableReservations(Show show);
}

public class CheckShowReservation : ICheckShowReservation
{
    private readonly IReservationRepository _repository;

    public CheckShowReservation(IReservationRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> GetNumberOfAvailableReservations(Show show)
    {
        var reservationsForTheShow = await _repository.GetReservationsForTheShow(show.Id);

        var currentNumberOfReservations = 0;

        foreach (var reservation in reservationsForTheShow)
        {
            currentNumberOfReservations += reservation.NumberOfReservations.Value;
        }

        return show.NumberOfPlaces - currentNumberOfReservations;
    }
}

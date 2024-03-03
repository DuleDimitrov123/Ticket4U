using AutoMapper;
using MediatR;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Shared.Application.Contracts.Persistence;

namespace Reservations.Application.Features.Reservations.Queries.GetReservations;

public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, IList<ReservationResponse>>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Reservation> _queryRepository;
    private readonly IQueryRepository<Show> _showQueryRepository;

    public GetReservationsQueryHandler(IMapper mapper,
        IQueryRepository<Reservation> queryRepository,
        IQueryRepository<Show> showQueryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
        _showQueryRepository = showQueryRepository;
    }

    public async Task<IList<ReservationResponse>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _queryRepository.GetAll();

        var reservationResponses = new List<ReservationResponse>();

        foreach (var reservation in reservations)
        {
            var reservationResponse = _mapper.Map<ReservationResponse>(reservation);
            var show = await _showQueryRepository.GetById(reservation.ShowId);

            reservationResponse.Show = _mapper.Map<ShowResponse>(show);
            reservationResponses.Add(reservationResponse);
        }

        return reservationResponses;
    }
}

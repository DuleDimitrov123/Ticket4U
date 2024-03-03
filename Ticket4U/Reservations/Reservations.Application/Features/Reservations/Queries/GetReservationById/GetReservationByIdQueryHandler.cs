using AutoMapper;
using MediatR;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationById;

public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Reservation> _queryRepository;
    private readonly IQueryRepository<Show> _showQueryRepository;

    public GetReservationByIdQueryHandler(IMapper mapper,
        IQueryRepository<Reservation> queryRepository,
        IQueryRepository<Show> showQueryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
        _showQueryRepository = showQueryRepository;
    }

    public async Task<ReservationResponse> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _queryRepository.GetById(request.ReservationId);

        if (reservation == null)
        {
            throw new NotFoundException(nameof(Reservation), request.ReservationId);
        }

        var reservationResponse = _mapper.Map<ReservationResponse>(reservation);
        var show = await _showQueryRepository.GetById(reservation.ShowId);

        reservationResponse.Show = _mapper.Map<ShowResponse>(show);

        return reservationResponse;
    }
}

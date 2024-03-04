using AutoMapper;
using MediatR;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationById;

public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Reservation> _reservationQueryRepository;
    private readonly IQueryRepository<Show> _showQueryRepository;
    private readonly IQueryRepository<User> _userQueryRepository;

    public GetReservationByIdQueryHandler(IMapper mapper,
        IQueryRepository<Reservation> reservationQueryRepository,
        IQueryRepository<Show> showQueryRepository,
        IQueryRepository<User> userQueryRepository)
    {
        _mapper = mapper;
        _reservationQueryRepository = reservationQueryRepository;
        _showQueryRepository = showQueryRepository;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<ReservationResponse> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationQueryRepository.GetById(request.ReservationId);

        if (reservation == null)
        {
            throw new NotFoundException(nameof(Reservation), request.ReservationId);
        }

        var reservationResponse = _mapper.Map<ReservationResponse>(reservation);
        var show = await _showQueryRepository.GetById(reservation.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), reservation.ShowId);
        }

        reservationResponse.Show = _mapper.Map<ShowResponse>(show);

        var user = await _userQueryRepository.GetById(reservation.UserId);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), reservation.UserId);
        }

        reservationResponse.UserId = user.ExternalId;

        return reservationResponse;
    }
}

using AutoMapper;
using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Shows;
using Reservations.Domain.Users;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationsByUserId;

public class GetReservationsByUserIdQueryHandler : IRequestHandler<GetReservationsByUserIdQuery, IList<ReservationResponse>>
{
    private readonly IMapper _mapper;
    private readonly IReservationQueryRepository _reservationQueryRepository;
    private readonly IQueryRepository<Show> _showQueryRepository;
    private readonly IQueryRepository<User> _userQueryRepository;

    public GetReservationsByUserIdQueryHandler(IMapper mapper,
        IReservationQueryRepository reservationQueryRepository,
        IQueryRepository<Show> showQueryRepository,
        IQueryRepository<User> userQueryRepository)
    {
        _mapper = mapper;
        _reservationQueryRepository = reservationQueryRepository;
        _showQueryRepository = showQueryRepository;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<IList<ReservationResponse>> Handle(GetReservationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationQueryRepository.GetReservationsByUserId(request.UserId);

        var reservationResponses = new List<ReservationResponse>();

        foreach (var reservation in reservations)
        {
            var reservationResponse = _mapper.Map<ReservationResponse>(reservation);
            var show = await _showQueryRepository.GetById(reservation.ShowId);

            if (show == null)
            {
                throw new NotFoundException(nameof(Show), reservation.ShowId);
            }

            reservationResponse.Show = _mapper.Map<ShowResponse>(show);
            reservationResponses.Add(reservationResponse);

            var user = await _userQueryRepository.GetById(reservation.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), reservation.UserId);
            }

            reservationResponse.UserId = user.ExternalId;
        }

        return reservationResponses;
    }
}

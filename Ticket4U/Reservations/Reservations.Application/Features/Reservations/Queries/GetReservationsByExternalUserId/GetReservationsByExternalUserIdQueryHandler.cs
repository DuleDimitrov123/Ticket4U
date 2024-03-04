using AutoMapper;
using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Shows;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationsByExternalUserId;

public class GetReservationsByExternalUserIdQueryHandler : IRequestHandler<GetReservationsByExternalUserIdQuery, IList<ReservationResponse>>
{
    private readonly IMapper _mapper;
    private readonly IReservationQueryRepository _reservationQueryRepository;
    private readonly IQueryRepository<Show> _showQueryRepository;
    private readonly IUserQueryRepository _userQueryRepository;

    public GetReservationsByExternalUserIdQueryHandler(IMapper mapper,
        IReservationQueryRepository reservationQueryRepository,
        IQueryRepository<Show> showQueryRepository,
        IUserQueryRepository userQueryRepository)
    {
        _mapper = mapper;
        _reservationQueryRepository = reservationQueryRepository;
        _showQueryRepository = showQueryRepository;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<IList<ReservationResponse>> Handle(GetReservationsByExternalUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userQueryRepository.GetUserByExternalId(request.ExternalUserId);

        if (user is null)
        {
            throw new NotFoundException(nameof(user), request.ExternalUserId);
        }

        var reservations = await _reservationQueryRepository.GetReservationsByUserId(user.Id);

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
        }

        return reservationResponses;
    }
}

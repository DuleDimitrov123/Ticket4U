using AutoMapper;
using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Responses;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationsByUserId;

public class GetReservationsByUserIdQueryHandler : IRequestHandler<GetReservationsByUserIdQuery, IList<ReservationResponse>>
{
    private readonly IMapper _mapper;
    private readonly IReservationQueryRepository _reservationQueryRepository;

    public GetReservationsByUserIdQueryHandler(IMapper mapper, IReservationQueryRepository reservationQueryRepository)
    {
        _mapper = mapper;
        _reservationQueryRepository = reservationQueryRepository;
    }

    public async Task<IList<ReservationResponse>> Handle(GetReservationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationQueryRepository.GetReservationsByUserId(request.UserId);

        return _mapper.Map<IList<ReservationResponse>>(reservations);
    }
}

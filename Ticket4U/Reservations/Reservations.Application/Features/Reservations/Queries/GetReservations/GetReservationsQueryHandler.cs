using AutoMapper;
using MediatR;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;
using Shared.Application.Contracts.Persistence;

namespace Reservations.Application.Features.Reservations.Queries.GetReservations;

public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, IList<ReservationResponse>>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Reservation> _queryRepository;

    public GetReservationsQueryHandler(IMapper mapper, IQueryRepository<Reservation> queryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
    }

    public async Task<IList<ReservationResponse>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _queryRepository.GetAll();

        return _mapper.Map<IList<ReservationResponse>>(reservations);
    }
}

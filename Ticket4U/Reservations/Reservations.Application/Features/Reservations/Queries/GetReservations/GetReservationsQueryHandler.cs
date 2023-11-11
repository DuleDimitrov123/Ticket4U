using AutoMapper;
using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;

namespace Reservations.Application.Features.Reservations.Queries.GetReservations;

public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, IList<ReservationResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Reservation> _repository;

    public GetReservationsQueryHandler(IMapper mapper, IRepository<Reservation> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IList<ReservationResponse>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _repository.GetAll();

        return _mapper.Map<IList<ReservationResponse>>(reservations);
    }
}

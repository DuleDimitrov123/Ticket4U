using AutoMapper;
using MediatR;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationById;

public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Reservation> _queryRepository;

    public GetReservationByIdQueryHandler(IMapper mapper, IQueryRepository<Reservation> queryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
    }

    public async Task<ReservationResponse> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _queryRepository.GetById(request.ReservationId);

        if (reservation == null)
        {
            throw new NotFoundException(nameof(Reservation), request.ReservationId);
        }

        return _mapper.Map<ReservationResponse>(reservation);
    }
}

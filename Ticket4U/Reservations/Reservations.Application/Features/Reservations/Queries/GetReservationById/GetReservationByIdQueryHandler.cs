using AutoMapper;
using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;
using Shared.Application.Exceptions;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationById;

public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Reservation> _repository;

    public GetReservationByIdQueryHandler(IMapper mapper, IRepository<Reservation> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ReservationResponse> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _repository.GetById(request.ReservationId);

        if (reservation == null)
        {
            throw new NotFoundException(nameof(Reservation), request.ReservationId);
        }

        return _mapper.Map<ReservationResponse>(reservation);
    }
}

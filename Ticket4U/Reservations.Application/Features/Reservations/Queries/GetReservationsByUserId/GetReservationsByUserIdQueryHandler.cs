using AutoMapper;
using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Application.Features.Reservations.Responses;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationsByUserId;

public class GetReservationsByUserIdQueryHandler : IRequestHandler<GetReservationsByUserIdQuery, IList<ReservationResponse>>
{
    private readonly IMapper _mapper;
    private readonly IReservationRepository _repository;

    public GetReservationsByUserIdQueryHandler(IMapper mapper, IReservationRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IList<ReservationResponse>> Handle(GetReservationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _repository.GetReservationsByUserId(request.UserId);

        return _mapper.Map<IList<ReservationResponse>>(reservations);
    }
}

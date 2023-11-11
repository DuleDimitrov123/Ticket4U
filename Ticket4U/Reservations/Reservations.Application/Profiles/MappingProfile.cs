using AutoMapper;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;

namespace Reservations.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Reservation, ReservationResponse>()
            .ForMember(
                reservationResponse => reservationResponse.NumberOfReservations,
                options => options.MapFrom(reservation => reservation.NumberOfReservations.Value))
            .ReverseMap();
    }
}

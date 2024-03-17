using AutoMapper;
using Reservations.Application.Features.Reservations.Responses;
using Reservations.Domain.Reservations;
using Reservations.Domain.Shows;

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

        CreateMap<Show, ShowResponse>()
            .ForMember(showResponse => showResponse.ShowId, opt => opt.MapFrom(show => show.ExternalId))
            .ForMember(showResponse => showResponse.ShowName, opt => opt.MapFrom(show => show.Name))
            .ForMember(showResponse => showResponse.ShowStartingDateTime, opt => opt.MapFrom(show => show.StartingDateTime))
            .ForMember(showResponse => showResponse.IsSoldOut, opt => opt.MapFrom(show => show.IsSoldOut));
    }
}

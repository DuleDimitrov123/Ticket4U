using AutoMapper;
using Shows.Application.Features.Categories.Commands.CreateCategory;
using Shows.Application.Features.Categories.Queries;
using Shows.Application.Features.Performers.Queries;
using Shows.Application.Features.Shows.Queries;
using Shows.Domain.Categories;
using Shows.Domain.Performers;
using Shows.Domain.Shows;

namespace Shows.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Performer, PerformerResponse>().ReverseMap();
        CreateMap<Performer, PerformerDetailResponse>().ReverseMap();
        CreateMap<PerformerInfo, PerformerInfoResponse>().ReverseMap();

        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, CategoryResponse>()
            .ForMember(
                categoryResponse => categoryResponse.Status, 
                options => options.MapFrom(category => category.Status.ToString()))
            .ReverseMap();

        CreateMap<Show, ShowResponse>()
            .ForMember(
                showResponse => showResponse.Status,
                options => options.MapFrom(show => show.Status.ToString()))
            .ForMember(
                showResponse => showResponse.NumberOfplaces,
                options => options.MapFrom(show => show.NumberOfPlaces.Value))
            .ForMember(
                showResponse => showResponse.TicketPriceCurrency,
                options => options.MapFrom(show => show.TicketPrice.Currency))
            .ForMember(
                showResponse => showResponse.TickerPriceAmount,
                options => options.MapFrom(show => show.TicketPrice.Amount))
            .ReverseMap();

        CreateMap<Show, ShowDetailResponse>()
            .ForMember(
                showResponse => showResponse.Status,
                options => options.MapFrom(show => show.Status.ToString()))
            .ForMember(
                showResponse => showResponse.NumberOfplaces,
                options => options.MapFrom(show => show.NumberOfPlaces.Value))
            .ForMember(
                showResponse => showResponse.TicketPriceCurrency,
                options => options.MapFrom(show => show.TicketPrice.Currency))
            .ForMember(
                showResponse => showResponse.TickerPriceAmount,
                options => options.MapFrom(show => show.TicketPrice.Amount))
            .ReverseMap();

        CreateMap<ShowMessage, ShowMessageResponse>().ReverseMap();
    }
}

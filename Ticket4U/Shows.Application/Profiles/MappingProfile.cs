using AutoMapper;
using Shows.Application.Features.Categories.Commands.CreateCategory;
using Shows.Application.Features.Categories.Queries;
using Shows.Application.Features.Performers.Queries;
using Shows.Domain.Categories;
using Shows.Domain.Performers;

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
    }
}

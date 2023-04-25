using AutoMapper;
using Shows.Application.Features.Performers.Queries;
using Shows.Domain.Performers;

namespace Shows.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Performer, PerformerResponse>().ReverseMap();
        CreateMap<Performer, PerformerDetailResponse>().ReverseMap();

        CreateMap<PerformerInfo, PerformerInfoResponse>().ReverseMap();
    }
}

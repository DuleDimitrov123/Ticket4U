using AutoMapper;
using Shows.Application.Performers.Commands;
using Shows.Application.Performers.Queries;
using Shows.Domain.Performers;

namespace Shows.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Performer, PerformerResponse>().ReverseMap();
    }
}

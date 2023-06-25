using AutoMapper;
using Shared.Domain.Events;
using Shows.Api.Requests.Shows;
using Shows.Application.Features.Shows.Commands.ChangeShowStatus;
using Shows.Application.Features.Shows.Commands.CreateShow;

namespace Shows.Api.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateShowRequest, CreateShowCommand>().ReverseMap();

        CreateMap<ChangeShowStatusCommand, ChangedShowStatusEvent>().ReverseMap();
    }
}

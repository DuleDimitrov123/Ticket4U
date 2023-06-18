using AutoMapper;
using Reservations.Application.Profiles;

namespace Reservations.UnitTests;

public class QueryCommandHandlerTestBase
{
    protected readonly IMapper _mapper;

    public QueryCommandHandlerTestBase()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }
}

using AutoMapper;
using Shows.Application.Profiles;

namespace Shows.UnitTests;

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

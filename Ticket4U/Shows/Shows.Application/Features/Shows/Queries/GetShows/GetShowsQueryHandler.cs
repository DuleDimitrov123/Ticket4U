using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Queries.GetShows;

public class GetShowsQueryHandler : IRequestHandler<GetShowsQuery, IList<ShowResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Show> _repository;

    public GetShowsQueryHandler(IMapper mapper, IRepository<Show> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IList<ShowResponse>> Handle(GetShowsQuery request, CancellationToken cancellationToken)
    {
        var shows = await _repository.GetAll();
        
        return _mapper.Map<IList<ShowResponse>>(shows);
    }
}

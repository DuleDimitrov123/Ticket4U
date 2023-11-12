using AutoMapper;
using MediatR;
using Shared.Application.Contracts.Persistence;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Queries.GetShows;

public class GetShowsQueryHandler : IRequestHandler<GetShowsQuery, IList<ShowResponse>>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Show> _queryRepository;

    public GetShowsQueryHandler(IMapper mapper, IQueryRepository<Show> queryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
    }

    public async Task<IList<ShowResponse>> Handle(GetShowsQuery request, CancellationToken cancellationToken)
    {
        var shows = await _queryRepository.GetAll();

        return _mapper.Map<IList<ShowResponse>>(shows);
    }
}

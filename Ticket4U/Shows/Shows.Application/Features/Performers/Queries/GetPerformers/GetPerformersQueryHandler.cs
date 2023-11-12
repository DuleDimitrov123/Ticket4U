using AutoMapper;
using MediatR;
using Shared.Application.Contracts.Persistence;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Queries.GetPerformers;

public class GetPerformersQueryHandler : IRequestHandler<GetPerformersQuery, IList<PerformerResponse>>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Performer> _queryRepository;

    public GetPerformersQueryHandler(IMapper mapper, IQueryRepository<Performer> queryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
    }

    public async Task<IList<PerformerResponse>> Handle(GetPerformersQuery request, CancellationToken cancellationToken)
    {
        var performers = await _queryRepository.GetAll();

        return _mapper.Map<IList<PerformerResponse>>(performers);
    }
}

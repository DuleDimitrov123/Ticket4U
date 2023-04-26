using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Queries.GetPerformers;

public class GetPerformersQueryHandler : IRequestHandler<GetPerformersQuery, IList<PerformerResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Performer> _repository;

    public GetPerformersQueryHandler(IMapper mapper, IRepository<Performer> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IList<PerformerResponse>> Handle(GetPerformersQuery request, CancellationToken cancellationToken)
    {
        var performers = await _repository.GetAll();

        return _mapper.Map<IList<PerformerResponse>>(performers);
    }
}

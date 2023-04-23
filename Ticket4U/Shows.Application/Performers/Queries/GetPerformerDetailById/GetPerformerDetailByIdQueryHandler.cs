using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;

namespace Shows.Application.Performers.Queries.GetPerformerDetailById;

public class GetPerformerDetailByIdQueryHandler : IRequestHandler<GetPerformerDetailByIdQuery, PerformerDetailResponse>
{
    private readonly IPerformerRepository _repository;
    private readonly IMapper _mapper;

    public GetPerformerDetailByIdQueryHandler(IMapper mapper, IPerformerRepository repository)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PerformerDetailResponse> Handle(GetPerformerDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var performer = await _repository.GetPerformerWithPerformerInfos(request.Id);

        return _mapper.Map<PerformerDetailResponse>(performer);
    }
}

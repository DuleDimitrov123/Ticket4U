using AutoMapper;
using MediatR;
using Shared.Application.Exceptions;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Queries.GetPerformerDetailById;

public class GetPerformerDetailByIdQueryHandler : IRequestHandler<GetPerformerDetailByIdQuery, PerformerDetailResponse>
{
    private readonly IPerformerQueryRepository _queryRepository;
    private readonly IMapper _mapper;

    public GetPerformerDetailByIdQueryHandler(IMapper mapper, IPerformerQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;
        _mapper = mapper;
    }

    public async Task<PerformerDetailResponse> Handle(GetPerformerDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var performer = await _queryRepository.GetPerformerWithPerformerInfos(request.Id);

        if (performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.Id);
        }

        return _mapper.Map<PerformerDetailResponse>(performer);
    }
}

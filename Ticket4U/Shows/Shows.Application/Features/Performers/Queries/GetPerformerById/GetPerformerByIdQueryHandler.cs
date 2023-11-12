using AutoMapper;
using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Queries.GetPerformerById;

public class GetPerformerByIdQueryHandler : IRequestHandler<GetPerformerByIdQuery, PerformerResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Performer> _queryRepository;

    public GetPerformerByIdQueryHandler(IMapper mapper, IQueryRepository<Performer> queryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
    }

    public async Task<PerformerResponse> Handle(GetPerformerByIdQuery request, CancellationToken cancellationToken)
    {
        var performer = await _queryRepository.GetById(request.Id);

        if (performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.Id);
        }

        return _mapper.Map<PerformerResponse>(performer);
    }
}

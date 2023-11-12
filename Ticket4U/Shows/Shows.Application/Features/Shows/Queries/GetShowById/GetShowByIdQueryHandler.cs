using AutoMapper;
using MediatR;
using Shared.Application.Contracts.Persistence;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Queries.GetShowById;

public class GetShowByIdQueryHandler : IRequestHandler<GetShowByIdQuery, ShowResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryRepository<Show> _queryRepository;

    public GetShowByIdQueryHandler(IMapper mapper, IQueryRepository<Show> queryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
    }

    public async Task<ShowResponse> Handle(GetShowByIdQuery request, CancellationToken cancellationToken)
    {
        var show = await _queryRepository.GetById(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        return _mapper.Map<ShowResponse>(show);
    }
}

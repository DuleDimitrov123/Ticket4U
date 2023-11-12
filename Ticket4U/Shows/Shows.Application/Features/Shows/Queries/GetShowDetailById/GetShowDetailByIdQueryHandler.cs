using AutoMapper;
using MediatR;
using Shared.Application.Exceptions;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Queries.GetShowDetailById;

public class GetShowDetailByIdQueryHandler : IRequestHandler<GetShowDetailByIdQuery, ShowDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IShowQueryRepository _queryRepository;

    public GetShowDetailByIdQueryHandler(IMapper mapper, IShowQueryRepository queryRepository)
    {
        _mapper = mapper;
        _queryRepository = queryRepository;
    }

    public async Task<ShowDetailResponse> Handle(GetShowDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var show = await _queryRepository.GetShowWithShowMessages(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        return _mapper.Map<ShowDetailResponse>(show);
    }
}

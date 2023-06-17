using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Queries.GetShowDetailById;

public class GetShowDetailByIdQueryHandler : IRequestHandler<GetShowDetailByIdQuery, ShowDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IShowRepository _repository;

    public GetShowDetailByIdQueryHandler(IMapper mapper, IShowRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ShowDetailResponse> Handle(GetShowDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetShowWithShowMessages(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        return _mapper.Map<ShowDetailResponse>(show);
    }
}

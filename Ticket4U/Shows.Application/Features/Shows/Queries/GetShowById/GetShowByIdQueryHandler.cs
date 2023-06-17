using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shared.Application.Exceptions;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Queries.GetShowById;

public class GetShowByIdQueryHandler : IRequestHandler<GetShowByIdQuery, ShowResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Show> _repository;

    public GetShowByIdQueryHandler(IMapper mapper, IRepository<Show> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ShowResponse> Handle(GetShowByIdQuery request, CancellationToken cancellationToken)
    {
        var show = await _repository.GetById(request.ShowId);

        if (show == null)
        {
            throw new NotFoundException(nameof(Show), request.ShowId);
        }

        return _mapper.Map<ShowResponse>(show);
    }
}

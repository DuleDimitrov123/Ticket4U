using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Queries.GetPerformerDetailById;

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

        if (performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.Id);
        }

        return _mapper.Map<PerformerDetailResponse>(performer);
    }
}

using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Queries.GetPerformerById;

public class GetPerformerByIdQueryHandler : IRequestHandler<GetPerformerByIdQuery, PerformerResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Performer> _repository;

    public GetPerformerByIdQueryHandler(IMapper mapper, IRepository<Performer> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PerformerResponse> Handle(GetPerformerByIdQuery request, CancellationToken cancellationToken)
    {
        var performer = await _repository.GetById(request.Id);

        if (performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.Id);
        }

        return _mapper.Map<PerformerResponse>(performer);
    }
}

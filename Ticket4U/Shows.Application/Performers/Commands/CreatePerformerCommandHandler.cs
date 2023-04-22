using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Performers;

namespace Shows.Application.Performers.Commands;

public class CreatePerformerCommandHandler : IRequestHandler<CreatePerformerCommand, Guid>
{
    private readonly IRepository<Performer> _repository;

    public CreatePerformerCommandHandler(IMapper mapper, IRepository<Performer> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreatePerformerCommand request, CancellationToken cancellationToken)
    {
        var newPerformer = Performer.Create(request.Name, request.PerformerInfos);

        newPerformer = await _repository.Add(newPerformer);

        return newPerformer.Id;
    }
}
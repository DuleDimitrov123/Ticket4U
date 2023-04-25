using AutoMapper;
using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Performers;

namespace Shows.Application.Features.Performers.Commands.CreatePerformer;

public class CreatePerformerCommandHandler : IRequestHandler<CreatePerformerCommand, Guid>
{
    private readonly IRepository<Performer> _repository;

    public CreatePerformerCommandHandler(IMapper mapper, IRepository<Performer> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreatePerformerCommand request, CancellationToken cancellationToken)
    {
        var newPerformer = Performer.Create(request.Name);

        foreach (KeyValuePair<string, string> entry in request.PerformerInfos)
        {
            newPerformer.AddPerformerInfo(PerformerInfo.Create(entry.Key, entry.Value));
        }

        newPerformer = await _repository.Add(newPerformer);

        return newPerformer.Id;
    }
}
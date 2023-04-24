﻿using MediatR;
using Shows.Application.Contracts.Persistance;
using Shows.Application.Exceptions;
using Shows.Domain.Performers;

namespace Shows.Application.Performers.Commands.DeletePerformerInfo;

public class DeletePerformerInfoCommandHandler : IRequestHandler<DeletePerformerInfoCommand, Unit>
{
    private readonly IPerformerRepository _repository;

    public DeletePerformerInfoCommandHandler(IPerformerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeletePerformerInfoCommand request, CancellationToken cancellationToken)
    {
        var performer = await _repository.GetPerformerWithPerformerInfos(request.PerformerId);

        if (performer == null)
        {
            throw new NotFoundException(nameof(Performer), request.PerformerId);
        }

        performer.RemovePerformerInfo(request.PerformerInfoNamesToDelete);
        await _repository.Update(performer);

        return Unit.Value;
    }
}

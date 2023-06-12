﻿using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Shows;

namespace Reservations.Application.Features.Shows.Commands.CreateShow;

public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, Guid>
{
    private readonly IRepository<Show> _repository;

    public CreateShowCommandHandler(IRepository<Show> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateShowCommand request, CancellationToken cancellationToken)
    {
        var category = Show.Create(request.Name, request.StartingDateTime, request.NumberOfPlaces, request.ExternalId);

        category = await _repository.Add(category);

        return category.Id;
    }
}

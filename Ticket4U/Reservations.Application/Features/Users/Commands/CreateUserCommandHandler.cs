using MediatR;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Users;

namespace Reservations.Application.Features.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IRepository<User> _repository;

    public CreateUserCommandHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email, request.ExternalId);

        user = await _repository.Add(user);

        return user.Id;
    }
}

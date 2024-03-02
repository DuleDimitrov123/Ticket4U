using MediatR;
using Reservations.Domain.Users;
using Shared.Application.Contracts.Persistence;

namespace Reservations.Application.Features.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly ICommandRepository<User> _commandRepository;

    public CreateUserCommandHandler(ICommandRepository<User> commandRepository)
    {
        _commandRepository = commandRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email, request.UserName, request.ExternalId);

        user = await _commandRepository.Add(user);

        return user.Id;
    }
}

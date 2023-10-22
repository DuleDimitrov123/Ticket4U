using MediatR;
using Shared.Infrastructure.Exceptions;
using Users.Application.Features.Users.Commands.RegistrateUser;

namespace Users.Api.BackgroundServices;

public class AddDefaultAdminUsersService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AddDefaultAdminUsersService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var user = new RegistrateUserCommand
        {
            FirstName = "Dusan",
            LastName = "Dimitrov",
            Email = "dusan.dimitrov@gmail.com",
            Password = "TestPass12*4NotReal",
            UserName = "DuleDimitrov",
            IsAdmin = true
        };

        using var scope = _serviceScopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        try
        {
            await mediator.Send(user);
        }
        catch (UserAlreadyExistsException ex)
        {
            //user already exists...had to throw an exception not to add user with CAP on RabbitMQ
        }
    }
}

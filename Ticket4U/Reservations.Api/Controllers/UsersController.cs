using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.Features.Users.Commands;
using Shared.Domain.Events;
using Shared.Domain.Events.Constants;

namespace Reservations.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //TODO: Event would be added instead of CreateUserRequest when connected with users microservice
    [HttpPost("CAPROUTE-CreateUserInReservations")]
    [CapSubscribe(Ticket4UDomainEventsConstants.NewUserCreated)]
    public async Task<ActionResult> CreateUser(CreatedUserEvent request)
    {
        var command = new CreateUserCommand()
        {
            Email = request.Email,
            UserName = request.UserName
        };

        var response = await _mediator.Send(command);

        return Ok(response);
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservations.Api.Requests.Users;
using Reservations.Application.Features.Users.Commands;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand()
        {
            Email = request.Email,
            ExternalId = request.ExternalId
        };

        var response = await _mediator.Send(command);

        return Ok(response);
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservations.Api.Requests.Reservations;
using Reservations.Application.Features.Reservations.Commands.CreateReservation;

namespace Reservations.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateReservation([FromBody] CreateReservationRequest request)
    {
        var command = new CreateReservationCommand()
        {
            UserId = request.UserId,
            ShowId = request.ShowId,
            NumberOfReservations = request.NumberOfReservations
        };

        var response = await _mediator.Send(command);

        return Ok(response);
    }
}

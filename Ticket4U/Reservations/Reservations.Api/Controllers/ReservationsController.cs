using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.Api.Requests.Reservations;
using Reservations.Application.Features.Reservations.Commands.CreateReservation;
using Reservations.Application.Features.Reservations.Commands.UpdateNumberOfReservations;
using Reservations.Application.Features.Reservations.Queries.GetReservationById;
using Reservations.Application.Features.Reservations.Queries.GetReservations;
using Reservations.Application.Features.Reservations.Queries.GetReservationsByUserId;
using Reservations.Application.Features.Reservations.Responses;

namespace Reservations.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
            ExternalUserId = request.ExternalUserId,
            ShowId = request.ExternalShowId,
            NumberOfReservations = request.NumberOfReservations
        };

        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ReservationResponse>>> GetAll()
    {
        var response = await _mediator.Send(new GetReservationsQuery());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ReservationResponse>> GetById([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetReservationByIdQuery() { ReservationId = id });

        return Ok(response);
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IList<ReservationResponse>>> GetByUserId([FromRoute] Guid userId)
    {
        var response = await _mediator.Send(new GetReservationsByUserIdQuery() { UserId = userId });

        return Ok(response);
    }

    [HttpPut("{reservationId}/newNumberOfResevations")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateNumberOfReservations([FromRoute] Guid reservationId, [FromBody] UpdateNumberOfReservationsRequest request)
    {
        await _mediator.Send(new UpdateNumberOfReservationsCommand()
        {
            Id = reservationId,
            NewNumberOfReservations = request.NewNumberOfReservations
        });

        return NoContent();
    }
}

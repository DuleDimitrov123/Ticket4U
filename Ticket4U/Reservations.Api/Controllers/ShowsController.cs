using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservations.Api.Requests.Shows;
using Reservations.Application.Features.Shows.Commands.CreateShow;

namespace Reservations.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShowsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShowsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateShow([FromBody] CreateShowRequest request)
    {
        var command = new CreateShowCommand()
        {
            Name = request.Name,
            StartingDateTime = request.StartingDateTime,
            NumberOfPlaces = request.NumberOfPlaces,
            ExternalId = request.ExternalId
        };

        var response = await _mediator.Send(command);

        return Ok(response);
    }
}

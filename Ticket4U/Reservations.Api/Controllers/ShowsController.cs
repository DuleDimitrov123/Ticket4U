﻿using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.Features.Shows.Commands.CreateShow;
using Reservations.Application.Features.Shows.Commands.UpdateShowStartingDateTime;
using Shared.Domain.Events;
using Shared.Domain.Events.Constants;

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

    [HttpPost("CAPROUTE-CreateShowInReservations")]
    [CapSubscribe(Ticket4UDomainEventsConstants.NewShowCreated)]
    public async Task<ActionResult> CreateShow(CreatedShowEvent createdShowEvent)
    {
        var command = new CreateShowCommand()
        {
            Name = createdShowEvent.Name,
            StartingDateTime = createdShowEvent.StartingDateTime,
            NumberOfPlaces = createdShowEvent.NumberOfPlaces,
            ExternalId = createdShowEvent.ExternalId
        };

        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPost("CAPROUTE-UpdateShowStartingDateTime")]
    [CapSubscribe(Ticket4UDomainEventsConstants.ShowStartingDateTimeUpdated)]
    public async Task<ActionResult> UpdateShowStartingDateTime(UpdatedShowsStartingDateTimeEvent updatedShowsStartingDateTimeEvent)
    {
        var command = new UpdateShowStartingDateTimeCommand()
        {
            ExternalShowId = updatedShowsStartingDateTimeEvent.ShowId,
            NewStartingDateTime = updatedShowsStartingDateTimeEvent.NewStartingDateTime
        };

        await _mediator.Send(command);

        return NoContent();
    }

    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<ActionResult> CreateShow([FromBody] CreateShowRequest request)
    //{
    //    var command = new CreateShowCommand()
    //    {
    //        Name = request.Name,
    //        StartingDateTime = request.StartingDateTime,
    //        NumberOfPlaces = request.NumberOfPlaces,
    //        ExternalId = request.ExternalId
    //    };

    //    var response = await _mediator.Send(command);

    //    return Ok(response);
    //}
}

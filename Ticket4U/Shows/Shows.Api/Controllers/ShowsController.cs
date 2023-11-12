using AutoMapper;
using DotNetCore.CAP;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Events;
using Shared.Domain.Events.Constants;
using Shows.Api.Requests.Shows;
using Shows.Application.Features.Shows.Commands.AddShowMessage;
using Shows.Application.Features.Shows.Commands.ChangeShowStatus;
using Shows.Application.Features.Shows.Commands.CreateShow;
using Shows.Application.Features.Shows.Commands.DeleteShow;
using Shows.Application.Features.Shows.Commands.UpdateShowLocation;
using Shows.Application.Features.Shows.Commands.UpdateShowName;
using Shows.Application.Features.Shows.Commands.UpdateShowPrice;
using Shows.Application.Features.Shows.Commands.UpdateShowStartingDateTime;
using Shows.Application.Features.Shows.Queries;
using Shows.Application.Features.Shows.Queries.GetShowById;
using Shows.Application.Features.Shows.Queries.GetShowDetailById;
using Shows.Application.Features.Shows.Queries.GetShows;

namespace Shows.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShowsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ShowsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{showId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<ShowResponse>> GetById([FromRoute] Guid showId)
        {
            var response = await _mediator.Send(new GetShowByIdQuery { ShowId = showId });

            return Ok(response);
        }

        [HttpGet("{showId}/detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<ShowDetailResponse>> GetDetailById([FromRoute] Guid showId)
        {
            var response = await _mediator.Send(new GetShowDetailByIdQuery { ShowId = showId });

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<ShowResponse>>> GetAll()
        {
            var response = await _mediator.Send(new GetShowsQuery());

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateShowRequest request)
        {
            var response = await _mediator.Send(_mapper.Map<CreateShowCommand>(request));

            return Ok(response);
        }

        [HttpPut("{showId}/newName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult> UpdateShowName([FromRoute] Guid showId, [FromBody] UpdateShowNameRequest request)
        {
            await _mediator.Send(new UpdateShowNameCommand()
            {
                Id = showId,
                NewName = request.NewName,
            });

            return NoContent();
        }

        [HttpPut("{showId}/newLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult> UpdateShowLocation([FromRoute] Guid showId, [FromBody] UpdateShowLocationRequest request)
        {
            await _mediator.Send(new UpdateShowLocationCommand()
            {
                Id = showId,
                NewLocation = request.NewLocation,
            });

            return NoContent();
        }

        [HttpPut("{showId}/newPrice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult> UpdateShowPrice([FromRoute] Guid showId, [FromBody] UpdateShowPriceRequest request)
        {
            await _mediator.Send(new UpdateShowPriceCommand()
            {
                Id = showId,
                NewAmount = request.NewAmount
            });

            return NoContent();
        }

        [HttpPut("{showId}/newStartingDateTime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult> UpdateShowStartingDateTime([FromRoute] Guid showId, [FromBody] UpdateShowStartingDateTimeRequest request)
        {
            await _mediator.Send(new UpdateShowStartingDateTimeCommand()
            {
                ShowId = showId,
                NewStartingDateTime = request.NewStartingDateTime,
            });

            return NoContent();
        }

        [HttpDelete("{showId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult> DeleteShow([FromRoute] Guid showId)
        {
            await _mediator.Send(new DeleteShowCommand() { Id = showId });

            return NoContent();
        }

        [HttpPost("{showId}/showMessages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult> AddShowMessage([FromRoute] Guid showId, [FromBody] AddShowMessageRequest request)
        {
            await _mediator.Send(new AddShowMessageCommand()
            {
                ShowId = showId,
                ShowMessageName = request.ShowMessageName,
                ShowMessageValue = request.ShowMessageValue
            });

            return NoContent();
        }

        [HttpPost("CAPROUTE-ChangedShowStatus")]
        [CapSubscribe(Ticket4UDomainEventsConstants.ChangedShowStatus)]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<ActionResult> ChangeShowStatus(ChangedShowStatusEvent changedShowStatusEvent)
        {
            var command = _mapper.Map<ChangeShowStatusCommand>(changedShowStatusEvent);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}

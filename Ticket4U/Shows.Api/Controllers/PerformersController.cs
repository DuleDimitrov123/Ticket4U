using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shows.Api.Requests.Performers;
using Shows.Application.Features.Performers.Commands.CreatePerformer;
using Shows.Application.Features.Performers.Commands.CreatePerformerInfo;
using Shows.Application.Features.Performers.Commands.DeletePerformerInfo;
using Shows.Application.Features.Performers.Queries;
using Shows.Application.Features.Performers.Queries.GetPerformerById;
using Shows.Application.Features.Performers.Queries.GetPerformerDetailById;
using Shows.Application.Features.Performers.Queries.GetPerformers;

namespace Shows.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PerformersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PerformersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<PerformerResponse>>> GetAll()
        {
            var response = await _mediator.Send(new GetPerformersQuery());

            return Ok(response);
        }

        [HttpGet("{performerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PerformerResponse>> GetById([FromRoute] Guid performerId)
        {
            var response = await _mediator.Send(new GetPerformerByIdQuery() { Id = performerId });

            return Ok(response);
        }

        [HttpGet("{performerId}/detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PerformerDetailResponse>> GetDetailById([FromRoute] Guid performerId)
        {
            var response = await _mediator.Send(new GetPerformerDetailByIdQuery() { Id = performerId });

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePerformerRequest request)
        {
            var command = new CreatePerformerCommand()
            {
                Name = request.Name,
                PerformerInfos = request.PerformerInfoRequests
            };

            var performerId = await _mediator.Send(command);

            return Ok(performerId);
        }

        [HttpPut("{performerId}/performer-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePerformerInfo([FromRoute] Guid performerId, [FromBody] UpdatePerformerInfoRequest request)
        {
            var command = new UpdatePerformerInfoCommand()
            {
                PerformerId = performerId,
                PerformerInfos = request.PerformerInfoRequests
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{performerId}/performer-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePerformerInfo([FromRoute] Guid performerId, [FromBody] DeletePerformerInfoRequest request)
        {
            var command = new DeletePerformerInfoCommand()
            {
                PerformerId = performerId,
                PerformerInfoNamesToDelete = request.PerformerInfoNamesToDelete
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}

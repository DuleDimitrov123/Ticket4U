using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shows.Api.Requests.Performers;
using Shows.Application.Performers.Commands;
using Shows.Application.Performers.Queries;
using Shows.Application.Performers.Queries.GetPerformerById;
using Shows.Domain.Performers;

namespace Shows.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PerformersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{performerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PerformerResponse>> GetById([FromRoute] Guid performerId)
        {
            var response = await _mediator.Send(new GetPerformerByIdQuery() { Id = performerId });

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        //TODO: Get performer with performer info

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePerformerRequest request)
        {
            var command = new CreatePerformerCommand()
            {
                Name = request.Name,
                PerformerInfos = request.CreatePerformerInfoRequests?.Select(
                    req => PerformerInfo.Create(req.Name, req.Value)).ToList()
            };

            var performerId = await _mediator.Send(command);

            return Ok(performerId);
        }
    }
}

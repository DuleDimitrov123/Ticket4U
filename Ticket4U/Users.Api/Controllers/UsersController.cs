using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Features.Users.Commands.AuthenticateUser;
using Users.Application.Features.Users.Commands.RegistrateUser;
using Users.Application.Models.Identity;

namespace Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrateUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}

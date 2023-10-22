using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Controllers.Requests;
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
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateUserRequest request)
        {
            var response = await _mediator.Send(new AuthenticateUserCommand()
            {
                Email = request.Email,
                Password = request.Password
            });

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegisterUserRequest request)
        {
            var response = await _mediator.Send(new RegistrateUserCommand()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
                IsAdmin = false
            });

            return Ok(response);
        }
    }
}

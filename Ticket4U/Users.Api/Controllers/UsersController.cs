using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts.Identity;
using Users.Application.Models.Identity;

namespace Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateRequest request)
        {
            var response = await _authenticationService.AuthenticateAsync(request);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
        {
            var response = await _authenticationService.RegistrateAsync(request);

            return Ok(response);
        }
    }
}

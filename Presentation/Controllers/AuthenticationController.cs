using Application.Commands.AuthenticationCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand model)
        {
            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand model)
        {
            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand model)
        {
            var response = await _mediator.Send(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}

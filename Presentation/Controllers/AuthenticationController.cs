using Application.Commands.AuthenticationCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCommand model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }
    }
}

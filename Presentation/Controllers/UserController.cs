using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using Application.Services.Models.Base;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IMediator mediator,
            UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromQuery] SearchBaseModel model)
        {
            var users = new GetAllUsersQuery(model);
            var response = await _mediator.Send(users);
            return Ok(response);
        }

        [HttpGet("{id}")]
        //[Authorize("admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = new GetUserByIdQuery(id);
            var response = await _mediator.Send(user);

            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                notFound => NotFound(response.AsT1));
        }
        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var getUser = await _userManager.GetUserAsync(User);
            if (getUser == null)
            {
                return NotFound($"Unable to load getUser with ID '{_userManager.GetUserId(User)}'.");
            }
            var user = new GetUserProfileQuery(getUser);
            var response = await _mediator.Send(user);
            return Ok(response);
        }
        [HttpPost("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileCommand userDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load getUser with ID '{_userManager.GetUserId(User)}'.");
            }
            userDto.User = user;
            var response = await _mediator.Send(userDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("role")]
        //[Authorize("admin")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleCommand userDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load getUser with ID '{_userManager.GetUserId(User)}'.");
            }
            userDto.User = user;

            var response = await _mediator.Send(userDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}

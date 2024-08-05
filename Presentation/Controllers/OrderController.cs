using Application.Commands.OrderCommands;
using Application.Queries.OrderQueries;
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
    public class OrderController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        //[Authorize("admin")]
        public async Task<IActionResult> GetAll([FromQuery] SearchBaseModel model)
        {
            var orders = new GetAllOrdersQuery(model);
            var response = await _mediator.Send(orders);
            return Ok(response);
        }

        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> GetOrderHistory([FromQuery] SearchBaseModel model)
        {
            var getUser = await _userManager.GetUserAsync(User);
            var orders = new GetOrdersHistoryQuery(model, getUser);
            var response = await _mediator.Send(orders);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = new GetOrderByIdQuery(id);
            var response = await _mediator.Send(order);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => NotFound(response.AsT1));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand orderDto)
        {
            var getUser = await _userManager.GetUserAsync(User);
            if (getUser == null)
            {
                return NotFound($"Unable to load getUser with ID '{_userManager.GetUserId(User)}'.");
            }
            orderDto.UserId = getUser.Id;
            var response = await _mediator.Send(orderDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
       
        [HttpPut("{id}")]
        [Authorize("admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderCommand orderDto)
        {
            orderDto.Id = id;
            var response = await _mediator.Send(orderDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}

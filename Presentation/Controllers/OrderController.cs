using Application.Commands.OrderCommands;
using Application.Queries.ComboQueries;
using Application.Queries.OrderQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderCommand orderDto)
        {
            var response = await _mediator.Send(orderDto);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery() { Id = id });
            return Ok(order);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateOrderCommand orderDto)
        {
            orderDto.Id = id;
            var response = await _mediator.Send(orderDto);
            return Ok(response);
        }
    }
}

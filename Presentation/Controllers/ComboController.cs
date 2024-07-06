using Application.Commands.ComboCommands;
using Application.Queries.CategoryQueries;
using Application.Queries.ComboQueries;
using Application.Queries.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ComboController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateComboCommand categoryDto)
        {
            var response = await _mediator.Send(categoryDto);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetAllCombosQuery());
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var combo = await _mediator.Send(new GetComboByIdQuery() { Id = id });
            return Ok(combo);
        }
    }
}

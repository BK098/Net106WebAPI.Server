using Application.Commands.ComboCommands;
using Application.Queries.ComboQueries;
using Application.Services.Models.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchBaseModel model)
        {
            var combos = new GetAllCombosQuery(model);
            var response = await _mediator.Send(combos);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var combo = new GetComboByIdQuery(id);
            var response = await _mediator.Send(combo);
            return response.Match<IActionResult>(
                 _ => Ok(response.AsT0),
                 error => NotFound(response.AsT1));

        }

        [HttpPost]
        //[Authorize("admin")]
        public async Task<IActionResult> Create([FromBody] CreateComboCommand comboDto)
        {
            var response = await _mediator.Send(comboDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("{id}")]
        //[Authorize("admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateComboCommand comboDto)
        {
            comboDto.Id = id;
            var response = await _mediator.Send(comboDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}

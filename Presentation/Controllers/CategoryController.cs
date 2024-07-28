using Application.Commands.CategoryCommands;
using Application.Queries.CategoryQueries;
using Application.Services.Models.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchBaseModel model)
        {
            var categories= new GetAllCategoriesQuery(model);
            var response = await _mediator.Send(categories);

            return Ok(response);
        }

        [HttpGet("{id}")] 

        public async Task<IActionResult> GetById(Guid id)
        {
            var category = new GetCategoryByIdQuery(id);
            var response = await _mediator.Send(category);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => NotFound(response.AsT1));
        }
        [HttpPost]
        //[Authorize("admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand categoryDto)
        {
            var response = await _mediator.Send(categoryDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("{id}")]
        //[Authorize("admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand categoryDto)
        {
            categoryDto.Id = id;
            var response = await _mediator.Send(categoryDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
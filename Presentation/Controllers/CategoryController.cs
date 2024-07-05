using Application.Commands.CategoryCommands;
using Application.Queries.CategoryQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryCommand categoryDto)
        {
            var response = await _mediator.Send(categoryDto);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var response = await _mediator.Send(new GetAllCategoriresQuery());
            return Ok(response);
        }
    }
}
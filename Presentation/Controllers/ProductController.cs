using Application.Commands.ProductCommands;
using Application.Queries.ProductQueries;
using Application.Services.Models.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchBaseModel model)
        {
            var products = new GetAllProductsQuery(model);
            var response = await _mediator.Send(products);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = new GetProductByIdQuery(id);
            var response = await _mediator.Send(product);

            return response.Match<IActionResult>(
                _ => Ok(response.AsT1),
                error => NotFound(response.AsT0));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand productDto)
        {
            var response = await _mediator.Send(productDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand productDto)
        {
            productDto.Id = id;
            var response = await _mediator.Send(productDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
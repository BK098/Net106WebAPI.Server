using Application.Commands.ProductCommands;
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
        // nên gọi sang service chứa tất cả tụi này, sau đó gọi đến dòng 21 send qua ProductCommand
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand productDto)
        {
            var product = await _mediator.Send(productDto);
            return product.Match<IActionResult>(
                _ => Ok(product.AsT0),
                error => BadRequest(product.AsT1),
                error => BadRequest(product.AsT2));
            //return Ok();
        }
    }
}

using Application.Commands.ProductCommand;
using Application.Services.Models.Product;
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
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand productDto)
        {
            return Ok(await _mediator.Send(productDto));
            /*var product = await _productService.CreateProduct(productDto);
            return product.Match<IActionResult>(
                _ => Ok(product.AsT0),
                error => BadRequest(product.AsT1),
                error => BadRequest(product.AsT2));*/
        }
    }
}

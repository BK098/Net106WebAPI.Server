using Application.Services.Contracts.Services.Commands;
using Application.Services.Models.Base;
using Application.Services.Models.ProductModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.ProductCommands
{
    public class CreateProductCommand : ProductForCreate, IRequest<UserMangeResponse> { }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, UserMangeResponse>
    {
        private readonly IProductCommandService _productService;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        public CreateProductCommandHandler(IProductCommandService productService, ILogger<CreateProductCommandHandler> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public async Task<UserMangeResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
            var result = await _productService.CreateProductAsync(request);
            return result;
        }
    }
}

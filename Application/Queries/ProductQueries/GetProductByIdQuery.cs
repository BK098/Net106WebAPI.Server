using Application.Services.Contracts.Services.Queries;
using Application.Services.Models.ProductModels;
using MediatR;

namespace Application.Queries.ProductQueries
{
    public class GetProductByIdQuery : ProductForViewItems, IRequest<ProductForViewItems> { }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductForViewItems>
    {
        private readonly IProductQueryService _productService;
        public GetProductByIdQueryHandler(IProductQueryService productService)
        {
            _productService = productService;
        }
        public async Task<ProductForViewItems> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetProductByIdAsync(request.Id);
        }
    }
}
using Application.Services.Contracts.Services.Queries;
using Application.Services.Models.ProductModels;
using MediatR;

namespace Application.Queries.ProductQueries
{
    public class GetAllProductsQuery : ProductForView, IRequest<ProductForView> { }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductForView>
    {
        private readonly IProductQueryService _productService;
        public GetAllProductsQueryHandler(IProductQueryService productService)
        {
            _productService = productService;
        }
        public async Task<ProductForView> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllProductsAsync();
        }
    }
}

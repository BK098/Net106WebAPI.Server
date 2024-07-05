using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.ProductModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.ProductQueries
{
    public class GetAllProductsQuery : ProductForView, IRequest<ProductForView> { }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductForView>
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;
        public GetAllProductsQueryHandler(IMapper mapper,
            ILocalizationMessage localization,
            IProductRepository repository,
            ILogger<GetAllProductsQueryHandler> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _productRepository = repository;
            _logger = logger;
        }
        public async Task<ProductForView> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Product> products = await _productRepository.GetAllProducts();
                IList<ProductForViewItems> items = _mapper.Map<IEnumerable<ProductForViewItems>>(products).ToList();
                ProductForView response = new ProductForView();
                response.Products = items;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}

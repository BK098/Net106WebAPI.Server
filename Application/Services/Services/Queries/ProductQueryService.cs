using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Contracts.Services.Queries;
using Application.Services.Models.ProductModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services.Services.Queries
{
    public class ProductQueryService: IProductQueryService
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductQueryService> _logger;
        public ProductQueryService(IMapper mapper,
            ILocalizationMessage localization,
            IProductRepository repository,
            ILogger<ProductQueryService> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _productRepository = repository;
            _logger = logger;
        }
        public async Task<ProductForView> GetAllProductsAsync()
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
                throw new NullReferenceException(nameof(GetAllProductsAsync));
            }
        }
        public async Task<ProductForViewItems> GetProductByIdAsync(Guid id)
        {
            try
            {
                Product product = await _productRepository.GetProductByIdAsync(id);
                ProductForViewItems item = _mapper.Map<ProductForViewItems>(product);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(GetAllProductsAsync));
            }
        }
    }
}
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.ProductModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.ProductQueries
{
    public class GetProductByIdQuery : ProductForViewItems, IRequest<ProductForViewItems> { }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductForViewItems>
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        public GetProductByIdQueryHandler(IMapper mapper,
            ILocalizationMessage localization,
            IProductRepository repository,
            ILogger<GetProductByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _productRepository = repository;
            _logger = logger;
        }
        public async Task<ProductForViewItems> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Product product = await _productRepository.GetProductByIdAsync(request.Id);
                ProductForViewItems item = _mapper.Map<ProductForViewItems>(product);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
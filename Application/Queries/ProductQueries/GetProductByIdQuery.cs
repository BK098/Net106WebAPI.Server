using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
using Application.Services.Models.Base;
using Application.Services.Models.ProductModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.Queries.ProductQueries
{
    public record GetProductByIdQuery(Guid id) : IRequest<OneOf<ProductForView, UserMangeResponse>> { }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, OneOf<ProductForView, UserMangeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        public GetProductByIdQueryHandler(IMapper mapper,
            IProductRepository repository,
            ILogger<GetProductByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _productRepository = repository;
            _logger = logger;
        }
        public async Task<OneOf<ProductForView, UserMangeResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(request.id);
                if (product == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "sản phẩm");
                }
                ProductForView item = _mapper.Map<ProductForView>(product);
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
using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.ProductModels;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.ProductCommands
{
    public class CreateProductCommand : ProductForCreate, IRequest<UserMangeResponse> { }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, UserMangeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<ProductForCreate> _validatorCreate;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        public CreateProductCommandHandler(IMapper mapper,
            ILocalizationMessage localization,
            IValidator<ProductForCreate> validatorCreate,
            ILogger<CreateProductCommandHandler> logger,
            IProductRepository repository)
        {
            _mapper = mapper;
            _localization = localization;
            _validatorCreate = validatorCreate;
            _logger = logger;
            _productRepository = repository;
        }

        public async Task<UserMangeResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorCreate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.CreateError, validationResult.Errors, _localization, "sản phẩm");
            }
            try
            {
                bool isProductExisted = await _productRepository.IsUniqueProductName(request.Name);
                if (!isProductExisted)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.Existed, validationResult.Errors, _localization, "sản phẩm");
                }
                Product product = _mapper.Map<Product>(request);
                product.DateAdded = DateTimeOffset.UtcNow;

                await _productRepository.CreateProductAsync(product);
                await _productRepository.SaveChangesAsync();

                return ResponseHelper.SuccessResponse(SuccessCode.CreateSuccess, "sản phẩm");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
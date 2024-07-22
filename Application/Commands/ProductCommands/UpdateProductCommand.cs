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
    public class UpdateProductCommand : ProductForUpdate, IRequest<UserMangeResponse> { }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UserMangeResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<ProductForUpdate> _validatorUpdate;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        public UpdateProductCommandHandler(IProductRepository productRepository,
            IMapper mapper, IValidator<ProductForUpdate> validatorCreate,
            ILogger<UpdateProductCommandHandler> logger,
            ILocalizationMessage localization)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validatorUpdate = validatorCreate;
            _logger = logger;
            _localization = localization;
        }

        public async Task<UserMangeResponse> Handle(UpdateProductCommand productDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(productDto);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.UpdateError, validationResult.Errors, _localization, "sản phẩm");
            }
            try
            {
                Product product = await _productRepository.GetProductByIdAsync(productDto.Id);
                if (product != null)
                {
                    if (product.Name != productDto.Name)
                    {
                        bool isProductExisted = await _productRepository.IsUniqueProductName(productDto.Name);
                        if (!isProductExisted)
                        {
                            return ResponseHelper.ErrorResponse(ErrorCode.Existed, validationResult.Errors, _localization, "sản phẩm");
                        }
                    }
                    product.LastUpdated = DateTimeOffset.UtcNow;

                    _mapper.Map(productDto, product);
                    _productRepository.UpdateProduct(product);
                    await _productRepository.SaveChangesAsync();
                    return ResponseHelper.SuccessResponse(SuccessCode.UpdateSuccess, "sản phẩm");
                }

                return ResponseHelper.ErrorResponse(ErrorCode.NotFound, validationResult.Errors, _localization, "sản phẩm");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
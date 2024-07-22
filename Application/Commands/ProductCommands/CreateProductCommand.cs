using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Repositories.Base;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductForCreate> _validatorCreate;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        public CreateProductCommandHandler(IMapper mapper,
            ILocalizationMessage localization,
            IValidator<ProductForCreate> validatorCreate,
            ILogger<CreateProductCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _localization = localization;
            _validatorCreate = validatorCreate;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserMangeResponse> Handle(CreateProductCommand productDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorCreate.ValidateAsync(productDto);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.CreateError, validationResult.Errors, _localization, "sản phẩm");
            }
            try
            {
                bool isProductExisted = await _unitOfWork.Product.IsUniqueProductName(productDto.Name);
                if (!isProductExisted)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.Existed, $"Sản phẩm {productDto.Name}");
                }
                var getCategoryId = await _unitOfWork.Category.GetCategoryByIdAsync(productDto.CategoryId);
                if (getCategoryId == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "thể loại");
                }
                Product product = _mapper.Map<Product>(productDto);
                product.DateAdded = DateTimeOffset.UtcNow;

                await _unitOfWork.Product.CreateProductAsync(product);
                await _unitOfWork.SaveChangesAsync();

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
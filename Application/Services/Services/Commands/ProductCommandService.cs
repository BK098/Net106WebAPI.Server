using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Contracts.Services.Commands;
using Application.Services.Models.Base;
using Application.Services.Models.ProductModels;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services.Services.Commands
{
    public class ProductCommandService : IProductCommandService
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<ProductForCreate> _validatorCreate;
        private readonly ILogger<ProductCommandService> _logger;
        private readonly IProductRepository _repository;
        public ProductCommandService(IMapper mapper, 
            ILocalizationMessage localization, 
            IValidator<ProductForCreate> validatorCreate, 
            ILogger<ProductCommandService> logger, 
            IProductRepository repository)
        {
            _mapper = mapper;
            _localization = localization;
            _validatorCreate = validatorCreate;
            _logger = logger;
            _repository = repository;
        }

        public async Task<UserMangeResponse> CreateProductAsync(ProductForCreate productDto)
        {
            var validationResult = await _validatorCreate.ValidateAsync(productDto);
            if (!validationResult.IsValid)
            {
                UserMangeResponse items = new UserMangeResponse();
                items.Message = "Có một số vấn đề khi thêm sản phẩm";
                items.Data = _localization.GetMessageData(items.Data, validationResult.Errors);
                items.Errors = _localization.GetMessageError(items.Errors, validationResult.Errors);
                items.IsSuccess = false;
                return items;
            }
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                product.DateAdded = DateTimeOffset.UtcNow;

                await _repository.CreateProductAsync(product);
                await _repository.SaveChangesAsync();
                return new UserMangeResponse
                {
                    Message = "Đã tạo sản phẩm thành công",
                    IsSuccess = true,
                    Errors = null,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(CreateProductAsync));
            }
        }
    }
}
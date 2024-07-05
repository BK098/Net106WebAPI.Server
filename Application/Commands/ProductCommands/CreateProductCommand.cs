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
                UserMangeResponse items = new UserMangeResponse();
                items.Message = "Có một số vấn đề khi thêm sản phẩm";
                items.Data = _localization.GetMessageData(items.Data, validationResult.Errors);
                items.Errors = _localization.GetMessageError(items.Errors, validationResult.Errors);
                items.IsSuccess = false;
                return items;
            }
            try
            {
                Product product = _mapper.Map<Product>(request);
                product.DateAdded = DateTimeOffset.UtcNow;

                await _productRepository.CreateProductAsync(product);
                await _productRepository.SaveChangesAsync();
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
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
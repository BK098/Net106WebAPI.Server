using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.ProductModels;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.Commands.ProductCommands
{
    public class CreateProductCommand : ProductForCreate, IRequest<OneOf<bool, ProductForCreate, ValidationResult>> { }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, OneOf<bool, ProductForCreate, ValidationResult>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ILocalizationMessageError _localization;
        private readonly IValidator<ProductForCreate> _validator;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepository productRepository,
            IMapper mapper,
            IValidator<ProductForCreate> validator,
            ILocalizationMessageError localization,
            ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
            _localization = localization;
            _logger = logger;
        }

        public async Task<OneOf<bool, ProductForCreate, ValidationResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                ProductForCreate items = new ProductForCreate();
                items.Errors = _localization.GetMessageError(items.Errors, validationResult.Errors);
                return items;
            }
            try
            {
                Product product = _mapper.Map<Product>(request);
                product.DateAdded = DateTimeOffset.UtcNow;

                await _productRepository.CreateProductAsync(product);
                await _productRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(CreateProductCommandHandler));
            }
        }
    }
}

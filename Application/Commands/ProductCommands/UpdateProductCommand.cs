using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.ProductModels;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.ProductCommands
{
    public class UpdateProductCommand : ProductForUpdate, IRequest<UserMangeResponse> { }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UserMangeResponse>
    {
        private readonly IProductRepository _comboRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<ProductForUpdate> _validatorUpdate;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        public UpdateProductCommandHandler(IProductRepository comboRepository,
            IMapper mapper, IValidator<ProductForUpdate> validatorCreate,
            ILogger<UpdateProductCommandHandler> logger,
            ILocalizationMessage localization)
        {
            _comboRepository = comboRepository;
            _mapper = mapper;
            _validatorUpdate = validatorCreate;
            _logger = logger;
            _localization = localization;
        }

        public async Task<UserMangeResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.UpdateError, validationResult.Errors, _localization, "sản phẩm");
            }
            try
            {
                var product = await _comboRepository.GetProductByIdAsync(request.Id);
                if (product == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, validationResult.Errors, _localization, "sản phẩm");
                }

                _mapper.Map(request, product);
                await _comboRepository.SaveChangesAsync();

                return ResponseHelper.SuccessResponse(SuccessCode.UpdateSuccess, "sản phẩm");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
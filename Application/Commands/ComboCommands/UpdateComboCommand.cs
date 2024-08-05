using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories.Base;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.ComboModels;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.ComboCommands
{
    public class UpdateComboCommand : ComboForUpdate, IRequest<UserMangeResponse> { }
    public class UpdateComboCommandHandler : IRequestHandler<UpdateComboCommand, UserMangeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<ComboForUpdate> _validatorUpdate;
        private readonly ILogger<UpdateComboCommandHandler> _logger;
        public UpdateComboCommandHandler(
            IMapper mapper, IValidator<ComboForUpdate> validatorCreate,
            ILogger<UpdateComboCommandHandler> logger,
            ILocalizationMessage localization, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _validatorUpdate = validatorCreate;
            _logger = logger;
            _localization = localization;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserMangeResponse> Handle(UpdateComboCommand comboDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(comboDto);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.UpdateError, validationResult.Errors, _localization, "Combo");
            }
            try
            {
                Combo combo = await _unitOfWork.Combo.GetComboByIdAsync(comboDto.Id);
                if (combo == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound,"Combo");
                }
                if (comboDto.Name != combo.Name)
                {
                    bool isComboExisted = await _unitOfWork.Combo.IsUniqueComboName(comboDto.Name);
                    if (!isComboExisted)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.Existed, $"Combo {comboDto.Name}");
                    }
                }

                combo.ProductCombos = combo.ProductCombos ?? new List<ProductCombo>();
                comboDto.ProductCombos = comboDto.ProductCombos ?? new List<ProductComboForUpdate>();

                var existingProductCombos = combo.ProductCombos;
                var updatedProductCombos = comboDto.ProductCombos;
                var productCombosToRemove = existingProductCombos.Where(pc => !updatedProductCombos.Any(upc => comboDto.Id == pc.ProductId)).ToList();

                //

                foreach (var productCombo in productCombosToRemove)
                {
                    combo.ProductCombos.Remove(productCombo);
                }

                foreach (var productComboDto in updatedProductCombos)
                {
                    var product = await _unitOfWork.Product.GetProductByIdAsync(productComboDto.ProductId);
                    if (product == null)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.NotFound, validationResult.Errors, _localization, "Product");
                    }
                    bool isProductInCombo = await _unitOfWork.Combo.IsProductComboExist(combo.Id, productComboDto?.ProductId);
                    if (isProductInCombo)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.Existed, $"Sản phẩm {product.Name}");
                    }
                    var existingProductCombo = combo.ProductCombos.FirstOrDefault(pc => pc.ProductId == comboDto.Id);
                    if (existingProductCombo != null)
                    {
                        existingProductCombo.Quantity = productComboDto.Quantity;
                    }
                    else
                    {
                        var newProductCombo = _mapper.Map<ProductCombo>(productComboDto);
                        combo.ProductCombos.Add(newProductCombo);
                    }
                }
             

                _mapper.Map(comboDto, combo);
                _unitOfWork.Combo.UpdateCombo(combo);
                await _unitOfWork.SaveChangesAsync();

                return ResponseHelper.SuccessResponse(SuccessCode.UpdateSuccess, "Combo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}

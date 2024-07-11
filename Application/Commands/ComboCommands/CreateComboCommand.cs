using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
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
    public class CreateComboCommand : ComboForCreate, IRequest<UserMangeResponse> { }
    public class CreateComboCommandHandler : IRequestHandler<CreateComboCommand, UserMangeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<ComboForCreate> _validatorCreate;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateComboCommandHandler> _logger;
        public CreateComboCommandHandler(IMapper mapper,
            ILocalizationMessage localization,
            IValidator<ComboForCreate> validatorCreate,
            ILogger<CreateComboCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _localization = localization;
            _validatorCreate = validatorCreate;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserMangeResponse> Handle(CreateComboCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorCreate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.CreateError, validationResult.Errors, _localization, "Combo");
            }
            try
            { 
                bool isComboExisted = await _unitOfWork.Combo.IsUniqueComboName(request.Name);
                if (!isComboExisted)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.Existed, validationResult.Errors, _localization, "Combo");
                }

                Combo combo = _mapper.Map<Combo>(request);
                combo.ProductCombos = _mapper.Map<ICollection<ProductCombo>>(request.ProductCombos);

                foreach (var productCombo in combo.ProductCombos)
                {
                    var product = await _unitOfWork.Product.GetProductByIdAsync(productCombo.ProductId.Value);
                    if (product == null)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.NotFound, validationResult.Errors, _localization, "Product");
                    }

                    bool isProductInCombo = await _unitOfWork.Combo.IsProductComboExist(combo.Id, productCombo?.ProductId);
                    if (isProductInCombo)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.Existed, validationResult.Errors, _localization, "ProductCombo");
                    }
                }
                await _unitOfWork.Combo.CreateComboAsync(combo);
                await _unitOfWork.Combo.SaveChangesAsync();

                return ResponseHelper.SuccessResponse(SuccessCode.CreateSuccess, "Combo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}

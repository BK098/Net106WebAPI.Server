using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.CategoryCommands
{
    public class UpdateCategoryCommand : CategoryForUpdate, IRequest<UserMangeResponse> { }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UserMangeResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<CategoryForUpdate> _validatorUpdate;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository,
            IMapper mapper, IValidator<CategoryForUpdate> validatorCreate,
            ILogger<UpdateCategoryCommandHandler> logger,
            ILocalizationMessage localization)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _validatorUpdate = validatorCreate;
            _logger = logger;
            _localization = localization;
        }

        public async Task<UserMangeResponse> Handle(UpdateCategoryCommand categoryDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.UpdateError, validationResult.Errors, _localization, "loại hàng");
            }
            try
            {
                Category category = await _categoryRepository.GetCategoryByIdAsync(categoryDto.Id);
                if (category == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "loại hàng");
                }
                if (categoryDto.Name != category.Name)
                {
                    bool isCategoryExisted = await _categoryRepository.IsUniqueCategoryName(categoryDto.Name);
                    if (!isCategoryExisted)
                    {
                        return ResponseHelper.ErrorResponse(ErrorCode.Existed, $"Loại hàng {categoryDto.Name}");
                    }
                }

                _mapper.Map(categoryDto, category);
                _categoryRepository.UpdateCategory(category);
                await _categoryRepository.SaveChangesAsync();
                return ResponseHelper.SuccessResponse(SuccessCode.UpdateSuccess, "Loại hàng");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.ErrorResponse(ErrorCode.OperationFailed, validationResult.Errors, _localization, "Loại hàng");
            }
        }
    }
}

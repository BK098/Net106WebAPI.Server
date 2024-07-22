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
    public class CreateCategoryCommand : CategoryForCreate, IRequest<UserMangeResponse> { }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, UserMangeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<CategoryForCreate> _validatorCreate;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;
        public CreateCategoryCommandHandler(IMapper mapper, ILocalizationMessage localization, IValidator<CategoryForCreate> validatorCreate, ICategoryRepository repository, ILogger<CreateCategoryCommandHandler> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _validatorCreate = validatorCreate;
            _categoryRepository = repository;
            _logger = logger;
        }

        public async Task<UserMangeResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorCreate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.CreateError, validationResult.Errors, _localization, "loại hàng");
            }
            try
            {
                bool isCategoryExisted = await _categoryRepository.IsUniqueCategoryName(request.Name);
                if (!isCategoryExisted)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.Existed, $"Loại hàng {request.Name}");
                }
                Category category = _mapper.Map<Category>(request);
                await _categoryRepository.CreateCategoryAsync(category);
                await _categoryRepository.SaveChangesAsync();
                return ResponseHelper.SuccessResponse(SuccessCode.CreateSuccess, "loại hàng");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}

using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Contracts.Services.Commands;
using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services.Services.Commands
{
    public class CategoryCommandService : ICategoryCommandService
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<CategoryForCreate> _validatorCreate;
        private readonly ILogger<CategoryCommandService> _logger;
        private readonly ICategoryRepository _repository;
        public CategoryCommandService(IMapper mapper,
            ILocalizationMessage localization,
            IValidator<CategoryForCreate> validatorCreate,
            ILogger<CategoryCommandService> logger,
            ICategoryRepository repository)
        {
            _mapper = mapper;
            _localization = localization;
            _validatorCreate = validatorCreate;
            _logger = logger;
            _repository = repository;
        }
        public async Task<UserMangeResponse> CreateCategoryAsync(CategoryForCreate categoryDto)
        {
            var validationResult = await _validatorCreate.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
            {
                UserMangeResponse items = new UserMangeResponse();
                items.Message = "Có vấn đề khi thêm thể loại";
                items.Data =_localization.GetMessageData(items.Data, validationResult.Errors);
                items.Errors = _localization.GetMessageError(items.Errors, validationResult.Errors);
                items.IsSuccess = false;
                return items;
            }
            try
            {
                Category category = _mapper.Map<Category>(categoryDto);
                await _repository.CreateCategoryAsync(category);
                await _repository.SaveChangesAsync();
                return new UserMangeResponse
                {
                    Message = "Đã tạo thể loại thành công",
                    IsSuccess = true,
                    Errors = null,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(CreateCategoryAsync));
            }
        }
    }
}

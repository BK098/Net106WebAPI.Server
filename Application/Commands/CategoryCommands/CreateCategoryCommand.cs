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
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;
        public CreateCategoryCommandHandler(IMapper mapper, ILocalizationMessage localization, IValidator<CategoryForCreate> validatorCreate, ICategoryRepository repository, ILogger<CreateCategoryCommandHandler> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _validatorCreate = validatorCreate;
            _repository = repository;
            _logger = logger;
        }

        public async Task<UserMangeResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorCreate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                UserMangeResponse items = new UserMangeResponse();
                items.Message = "Có vấn đề khi thêm loại sản phẩm";
                items.Data = _localization.GetMessageData(items.Data, validationResult.Errors);
                items.Errors = _localization.GetMessageError(items.Errors, validationResult.Errors);
                items.IsSuccess = false;
                return items;
            }
            try
            {
                Category category = _mapper.Map<Category>(request);
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
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}

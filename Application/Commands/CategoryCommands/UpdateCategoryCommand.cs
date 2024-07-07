using Application.Commands.CategoryCommands;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Application.Commands.CategoryCommands
{
    public class UpdateCategoryCommand : CategoryForUpdate, IRequest<UserMangeResponse>
    {
        [JsonIgnore]
        public Guid CategoryId { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UserMangeResponse>
    {
        private readonly ICategoryRepository _comboRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<CategoryForUpdate> _validatorUpdate;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;
        public UpdateCategoryCommandHandler(ICategoryRepository comboRepository,
            IMapper mapper, IValidator<CategoryForUpdate> validatorCreate,
            ILogger<UpdateCategoryCommandHandler> logger,
            ILocalizationMessage localization)
        {
            _comboRepository = comboRepository;
            _mapper = mapper;
            _validatorUpdate = validatorCreate;
            _logger = logger;
            _localization = localization;
        }

        public async Task<UserMangeResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                UserMangeResponse items = new UserMangeResponse();
                items.Message = "Có một số vấn đề khi cập nhật Thể Loại";
                items.Data = _localization.GetMessageData(items.Data, validationResult.Errors);
                items.Errors = _localization.GetMessageError(items.Errors, validationResult.Errors);
                items.IsSuccess = false;
                return items;
            }
            try
            {
                var existingCategory = await _comboRepository.GetCategoryByIdAsync(request.CategoryId);
                if (existingCategory == null)
                {
                    return new UserMangeResponse
                    {
                        Message = "Thể Loại Này không tồn tại",
                        IsSuccess = false,
                        Errors = new Dictionary<string, List<object>>() { { "s", new List<object> { $"Không Tìm Thấy Thể Loại Có Id : '{request.CategoryId}'" } } },
                        Data = null
                    };
                }

                _mapper.Map(request, existingCategory);
                await _comboRepository.SaveChangesAsync();

                return new UserMangeResponse
                {
                    Message = "Đã tạo Category thành công",
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

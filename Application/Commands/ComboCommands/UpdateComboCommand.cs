using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.ComboModels;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Application.Commands.ComboCommands
{
    public class UpdateComboCommand : ComboForUpdate, IRequest<UserMangeResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
    public class UpdateComboCommandHandler : IRequestHandler<UpdateComboCommand, UserMangeResponse>
    {
        private readonly IComboRepository _comboRepository;
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<ComboForUpdate> _validatorUpdate;
        private readonly ILogger<UpdateComboCommandHandler> _logger;
        public UpdateComboCommandHandler(IComboRepository comboRepository,
            IMapper mapper, IValidator<ComboForUpdate> validatorCreate,
            ILogger<UpdateComboCommandHandler> logger,
            ILocalizationMessage localization)
        {
            _comboRepository = comboRepository;
            _mapper = mapper;
            _validatorUpdate = validatorCreate;
            _logger = logger;
            _localization = localization;
        }

        public async Task<UserMangeResponse> Handle(UpdateComboCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                UserMangeResponse items = new UserMangeResponse();
                items.Message = "Có một số vấn đề khi cập nhật Combo";
                items.Data = _localization.GetMessageData(items.Data, validationResult.Errors);
                items.Errors = _localization.GetMessageError(items.Errors, validationResult.Errors);
                items.IsSuccess = false;
                return items;
            }
            try
            {
                var existingCombo = await _comboRepository.GetComboByIdAsync(request.Id);
                if (existingCombo == null)
                {
                    return new UserMangeResponse
                    {
                        Message = "Combo không tồn tại",
                        IsSuccess = false,
                        Errors = new Dictionary<string, List<object>>() { { "s", new List<object> { $"Không Tìm Thấy Combo Có Id : '{request.Id}'", 9 } } },
                        Data = null
                    };
                }
                _mapper.Map(request, existingCombo);
                existingCombo.ProductItems = _mapper.Map<ICollection<ProductItem>>(request.ComboItems);
                await _comboRepository.SaveChangesAsync();
                return new UserMangeResponse
                {
                    Message = "Đã tạo Combo thành công",
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

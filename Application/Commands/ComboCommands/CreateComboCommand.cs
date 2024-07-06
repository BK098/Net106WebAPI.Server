using Application.Services.Contracts.Repositories;
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
        private readonly IComboRepository _repository;
        private readonly ILogger<CreateComboCommandHandler> _logger;
        public CreateComboCommandHandler(IMapper mapper, ILocalizationMessage localization, IValidator<ComboForCreate> validatorCreate, IComboRepository repository, ILogger<CreateComboCommandHandler> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _validatorCreate = validatorCreate;
            _repository = repository;
            _logger = logger;
        }
        public async Task<UserMangeResponse> Handle(CreateComboCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorCreate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                UserMangeResponse items = new UserMangeResponse();
                items.Message = "Có một số vấn đề khi thêm Combo";
                items.Data = _localization.GetMessageData(items.Data, validationResult.Errors);
                items.Errors = _localization.GetMessageError(items.Errors, validationResult.Errors);
                items.IsSuccess = false;
                return items;
            }
            try
            {
                Combo product = _mapper.Map<Combo>(request);

                await _repository.CreateComboAsync(product);
                await _repository.SaveChangesAsync();
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

using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.ProductModels;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Application.Commands.ProductCommands
{
    public class UpdateProductCommand : ProductForUpdate, IRequest<UserMangeResponse>
    {
        [JsonIgnore]
        public Guid ProductId { get; set; }
    }
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
                UserMangeResponse items = new UserMangeResponse();
                items.Message = "Có một số vấn đề khi cập nhật Sản Phẩm";
                items.Data = _localization.GetMessageData(items.Data, validationResult.Errors);
                items.Errors = _localization.GetMessageError(items.Errors, validationResult.Errors);
                items.IsSuccess = false;
                return items;
            }
            try
            {
                var existingProduct = await _comboRepository.GetProductByIdAsync(request.ProductId);
                if (existingProduct == null)
                {
                    return new UserMangeResponse
                    {
                        Message = "Sản Phẩm không tồn tại",
                        IsSuccess = false,
                        Errors = new Dictionary<string, List<object>>() { { "s", new List<object> { $"Không Tìm Thấy sản Phẩm Có Id : '{request.ProductId}'" } } },
                        Data = null
                    };
                }

                _mapper.Map(request, existingProduct);
                await _comboRepository.SaveChangesAsync();

                return new UserMangeResponse
                {
                    Message = "Đã tạo Product thành công",
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
using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Repositories.Base;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.OrderModels;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.OrderCommands
{
    public class UpdateOrderCommand : OrderForUpdate, IRequest<UserMangeResponse> { }
    public class UpdateOrderCommandhandler : IRequestHandler<UpdateOrderCommand, UserMangeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<OrderForUpdate> _validatorUpdate;
        private readonly ILogger<UpdateOrderCommandhandler> _logger;
        public UpdateOrderCommandhandler(IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILocalizationMessage localization,
            IValidator<OrderForUpdate> validatorUpdate,
            ILogger<UpdateOrderCommandhandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localization = localization;
            _validatorUpdate = validatorUpdate;
            _logger = logger;
        }
        public async Task<UserMangeResponse> Handle(UpdateOrderCommand orderDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(orderDto);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.UpdateError, validationResult.Errors, _localization, "Order");
            }
            try
            {
                var order = await _unitOfWork.Order.GetOrderByIdAsync(orderDto.Id);
                if (order == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "Order");
                }
                order.OrderDate = orderDto.OrderDate;
                order.Status = orderDto.Status;

                await _unitOfWork.Order.UpdateOrder(order);
                await _unitOfWork.SaveChangesAsync();
                return ResponseHelper.SuccessResponse(SuccessCode.UpdateSuccess, "Order");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}

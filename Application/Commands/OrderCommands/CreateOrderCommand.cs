﻿
using Application.Services.Contracts.Repositories.Base;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.OrderModels;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Helpers;
using Application.Enums;
using Domain.Entities;
using Domain.Enums;

namespace Application.Commands.OrderCommands
{
    public class CreateOrderCommand : OrderForCreate, IRequest<UserMangeResponse> { }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, UserMangeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IValidator<OrderForCreate> _validatorCreate;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IMapper mapper,
            ILocalizationMessage localization,
            IValidator<OrderForCreate> validatorCreate,
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            ILogger<CreateOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _validatorCreate = validatorCreate;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<UserMangeResponse> Handle(CreateOrderCommand orderDto, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorCreate.ValidateAsync(orderDto);
            if (!validationResult.IsValid)
            {
                return ResponseHelper.ErrorResponse(ErrorCode.CreateError, validationResult.Errors, _localization, "Order");
            }
            try
            {

                Order order = _mapper.Map<Order>(orderDto);
                order.OrderDate = DateTimeOffset.UtcNow;
                order.Status = OrderStatus.Processing;
                order.UserId = "3057932c-6849-40d1-bc8f-89b0f7d8472e";
                order.OrderItems = _mapper.Map<ICollection<OrderItem>>(orderDto.OrderItems);
                foreach (var orderItem in order.OrderItems)
                {
                    if (orderItem.ProductId != null)
                    {
                        Product product = await _unitOfWork.Product.GetProductByIdAsync(orderItem.ProductId.Value);
                        if (product == null)
                        {
                            return ResponseHelper.ErrorResponse(ErrorCode.NotFound, validationResult.Errors, _localization, "sản phẩm");
                        }
                    }
                    if (orderItem.ComboId != null)
                    {
                        Combo combo = await _unitOfWork.Combo.GetComboByIdAsync(orderItem.ComboId.Value);
                        if (combo == null)
                        {
                            return ResponseHelper.ErrorResponse(ErrorCode.NotFound, validationResult.Errors, _localization, "combo");
                        }
                    }
                    orderItem.TotalPrice += orderItem.UnitPrice * orderItem.Quantity;
                }
                order.TotalAmount = order.OrderItems.Count > 0 ? order.OrderItems.Sum(x => x.TotalPrice) : 0;

                await _unitOfWork.Order.CreateOrderAsync(order);
                await _unitOfWork.Order.SaveChangesAsync();

                return ResponseHelper.SuccessResponse(SuccessCode.CreateSuccess, "sản phẩm");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
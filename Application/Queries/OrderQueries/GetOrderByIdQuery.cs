﻿using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.OrderModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.Queries.OrderQueries
{
    public class GetOrderByIdQuery : OrderForViewItems, IRequest<OneOf<UserMangeResponse,OrderForViewItems>> { }
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OneOf<UserMangeResponse, OrderForViewItems>>
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrderByIdQueryHandler> _logger;
        public GetOrderByIdQueryHandler(IMapper mapper,
            ILocalizationMessage localization,
            IOrderRepository repository,
            ILogger<GetOrderByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _orderRepository = repository;
            _logger = logger;
        }
        public async Task<OneOf<UserMangeResponse, OrderForViewItems>> Handle(GetOrderByIdQuery orderDto, CancellationToken cancellationToken)
        {

            try
            {
                Order order = await _orderRepository.GetOrderByIdAsync(orderDto.Id);
                if (order == null)
                {
                    //return ResponseHelper.ErrorResponse(ErrorCode.NotFound, validationResult.Errors, _localization, "Order");
                }
                OrderForViewItems item = _mapper.Map<OrderForViewItems>(order);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
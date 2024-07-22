using Application.Enums;
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
    public record GetOrderByIdQuery(Guid id) : IRequest<OneOf<UserMangeResponse, OrderForView>> { }
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OneOf<UserMangeResponse, OrderForView>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrderByIdQueryHandler> _logger;
        public GetOrderByIdQueryHandler(IMapper mapper,
            IOrderRepository repository,
            ILogger<GetOrderByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _orderRepository = repository;
            _logger = logger;
        }
        public async Task<OneOf<UserMangeResponse, OrderForView>> Handle(GetOrderByIdQuery orderDto, CancellationToken cancellationToken)
        {
            try
            {
                Order order = await _orderRepository.GetOrderByIdAsync(orderDto.id);
                if (order == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "Order");
                }
                OrderForView item = _mapper.Map<OrderForView>(order);
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

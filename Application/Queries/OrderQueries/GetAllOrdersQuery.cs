using Application.Services.Contracts.Repositories;
using Application.Services.Models.OrderModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.OrderQueries
{
    public class GetAllOrdersQuery : OrderForView, IRequest<OrderForView> { }
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, OrderForView>
    {
        private readonly IOrderRepository _orderReponsitory;
        private readonly ILogger<GetAllOrdersQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository orderReponsitory,
            ILogger<GetAllOrdersQueryHandler> logger,
            IMapper mapper)
        {
            _orderReponsitory = orderReponsitory;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OrderForView> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Order> orders = await _orderReponsitory.GetAllOrdersAsync();
                IList<OrderForViewItems> items = _mapper.Map<IEnumerable<OrderForViewItems>>(orders).ToList();
                OrderForView response = new OrderForView();
                response.Orders = items;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}

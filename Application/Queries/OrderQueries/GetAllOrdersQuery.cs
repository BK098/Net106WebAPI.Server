using Application.Services.Contracts.Repositories;
using Application.Services.Models.Base;
using Application.Services.Models.ComboModels;
using Application.Services.Models.OrderModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.OrderQueries
{
    public record GetAllOrdersQuery(SearchBaseModel SearchModel) : IRequest<PaginatedList<OrderForView>> { }
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PaginatedList<OrderForView>>
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

        public async Task<PaginatedList<OrderForView>> Handle(GetAllOrdersQuery orderDto, CancellationToken cancellationToken)
        {
            try
            {
                var combos = _orderReponsitory.GetAllOrders(orderDto.SearchModel, cancellationToken);
                var paginatedCombos = await PaginatedList<Order>.CreateAsync(
                    combos,
                    orderDto.SearchModel.PageIndex,
                    orderDto.SearchModel.PageSize,
                    cancellationToken);

                var items = new PaginatedList<OrderForView>(
                    _mapper.Map<List<OrderForView>>(paginatedCombos.Items),
                    orderDto.SearchModel.PageIndex,
                    orderDto.SearchModel.PageSize,
                    paginatedCombos.TotalCount);

                if (items == null)
                {

                }
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
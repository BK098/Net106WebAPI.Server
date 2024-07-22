using Application.Services.Contracts.Repositories;
using Application.Services.Models.Base;
using Application.Services.Models.OrderModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.OrderQueries
{
    public record GetOrdersHistoryQuery(SearchBaseModel SearchModel, AppUser User) : IRequest<PaginatedList<OrderForView>> { }
    public class GetOrdersHistoryQueryHandler : IRequestHandler<GetOrdersHistoryQuery, PaginatedList<OrderForView>>
    {
        private readonly IOrderRepository _orderReponsitory;
        private readonly ILogger<GetOrdersHistoryQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetOrdersHistoryQueryHandler(IOrderRepository orderReponsitory,
            ILogger<GetOrdersHistoryQueryHandler> logger,
            IMapper mapper)
        {
            _orderReponsitory = orderReponsitory;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedList<OrderForView>> Handle(GetOrdersHistoryQuery orderDto, CancellationToken cancellationToken)
        {
            try
            {
                var combos = _orderReponsitory.GetAllOrdersHitory(orderDto.SearchModel, orderDto.User.Id,cancellationToken);
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

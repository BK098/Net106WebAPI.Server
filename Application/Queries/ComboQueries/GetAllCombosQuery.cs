using Application.Services.Contracts.Repositories;
using Application.Services.Models.Base;
using Application.Services.Models.ComboModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.ComboQueries
{
    public record GetAllCombosQuery(SearchBaseModel SearchModel) : IRequest<PaginatedList<ComboForView>> { }
    public class GetAllCombosQueryHandler : IRequestHandler<GetAllCombosQuery, PaginatedList<ComboForView>>
    {
        private readonly IComboRepository _comboReponsitory;
        private readonly ILogger<GetAllCombosQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllCombosQueryHandler(IComboRepository comboReponsitory,
            ILogger<GetAllCombosQueryHandler> logger,
            IMapper mapper)
        {
            _comboReponsitory = comboReponsitory;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ComboForView>> Handle(GetAllCombosQuery comboDto, CancellationToken cancellationToken)
        {
            try
            {
                var combos = _comboReponsitory.GetAllCombosAsync(comboDto.SearchModel, cancellationToken);
                var paginatedCategories = await PaginatedList<Combo>.CreateAsync(
                    combos,
                    comboDto.SearchModel.PageIndex,
                    comboDto.SearchModel.PageSize,
                    cancellationToken);

                var items = new PaginatedList<ComboForView>(
                    _mapper.Map<List<ComboForView>>(paginatedCategories.Items),
                    comboDto.SearchModel.PageIndex,
                    comboDto.SearchModel.PageSize,
                    paginatedCategories.TotalCount);

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

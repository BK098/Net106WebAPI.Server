using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Queries;
using Application.Services.Models.ComboModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.ComboQueries
{
    public class GetAllCombosQuery :ComboForView, IRequest<ComboForView>{ }
    public class GetAllCombosQueryHandler : IRequestHandler<GetAllCombosQuery, ComboForView>
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

        public async Task<ComboForView> Handle(GetAllCombosQuery request, CancellationToken cancellationToken)
        {
          try
            {
                IEnumerable<Combo> combos = await _comboReponsitory.GetAllCombosAsync();
                IList<ComboForViewItems> items = _mapper.Map<IEnumerable<ComboForViewItems>>(combos).ToList();

                ComboForView response = new ComboForView();
                response.Combos = items;
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

using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.ComboModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.ComboQueries
{
    public class GetComboByIdQuery : ComboForViewItems, IRequest<ComboForViewItems> { }
    public class GetComboByIdQueryHandler : IRequestHandler<GetComboByIdQuery, ComboForViewItems>
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly IComboRepository _comboRepository;
        private readonly ILogger<GetComboByIdQueryHandler> _logger;
        public GetComboByIdQueryHandler(IMapper mapper,
            ILocalizationMessage localization,
            IComboRepository repository,
            ILogger<GetComboByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _comboRepository = repository;
            _logger = logger;
        }
        public async Task<ComboForViewItems> Handle(GetComboByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Combo product = await _comboRepository.GetComboByIdAsync(request.Id);
                ComboForViewItems item = _mapper.Map<ComboForViewItems>(product);
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

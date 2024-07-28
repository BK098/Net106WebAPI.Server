using Application.Enums;
using Application.Helpers;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.Base;
using Application.Services.Models.ComboModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.Queries.ComboQueries
{
    public record GetComboByIdQuery(Guid id) : IRequest<OneOf<ComboForView, UserMangeResponse>> { }
    public class GetComboByIdQueryHandler : IRequestHandler<GetComboByIdQuery, OneOf<ComboForView, UserMangeResponse>>
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
        public async Task<OneOf<ComboForView, UserMangeResponse>> Handle(GetComboByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var combo = await _comboRepository.GetComboByIdAsync(request.id);
                if (combo == null)
                {
                    return ResponseHelper.ErrorResponse(ErrorCode.NotFound, "combo");
                }
                ComboForView item = _mapper.Map<ComboForView>(combo);
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

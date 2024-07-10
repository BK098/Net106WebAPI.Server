using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Base;
using Application.Services.Models.CategoryModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.CategoryQueries
{
    public class GetCategoryByIdQuery : CategoryForViewItems, IRequest<CategoryForViewItems> { }
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryForViewItems>
    {
        private readonly IMapper _mapper;
        private readonly ILocalizationMessage _localization;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoryByIdQueryHandler> _logger;
        public GetCategoryByIdQueryHandler(IMapper mapper,
            ILocalizationMessage localization,
            ICategoryRepository repository,
            ILogger<GetCategoryByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _localization = localization;
            _categoryRepository = repository;
            _logger = logger;
        }
        public async Task<CategoryForViewItems> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Category category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
                CategoryForViewItems item = _mapper.Map<CategoryForViewItems>(category);
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

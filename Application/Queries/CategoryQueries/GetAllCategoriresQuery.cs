using Application.Services.Contracts.Repositories;
using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.CategoryQueries
{
    public record GetAllCategoriesQuery(SearchBaseModel SearchModel) : IRequest<PaginatedList<CategoryForView>>;
    public class GetAllCategoriresQueryHandler : IRequestHandler<GetAllCategoriesQuery, PaginatedList<CategoryForView>>
    {

        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetAllCategoriresQueryHandler> _logger;

        public GetAllCategoriresQueryHandler(ICategoryRepository categoryRepository,
            IMapper mapper,
            ILogger<GetAllCategoriresQueryHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PaginatedList<CategoryForView>> Handle(GetAllCategoriesQuery categoryDto, CancellationToken cancellationToken)
        {
            try
            {
                var categories = _categoryRepository.GetAllCategories(categoryDto.SearchModel, cancellationToken);

                var paginatedCategories = await PaginatedList<Category>.CreateAsync(
                    categories,
                    categoryDto.SearchModel.PageIndex,
                    categoryDto.SearchModel.PageSize,
                    cancellationToken);

                var items = new PaginatedList<CategoryForView>(
                    _mapper.Map<List<CategoryForView>>(paginatedCategories.Items),
                    categoryDto.SearchModel.PageIndex,
                    categoryDto.SearchModel.PageSize,
                    paginatedCategories.TotalCount);

                if (items == null)
                {

                }
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching categories: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
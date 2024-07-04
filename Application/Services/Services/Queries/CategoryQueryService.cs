using Application.Queries.CategoryQueries;
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Services.Queries;
using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;
using Application.Services.Models.CategoryModels.Base;
using Application.Services.Models.ProductModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Services.Queries
{
    public class CategoryQueryService : ICategoryQueryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryQueryService> _logger;

        public CategoryQueryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryQueryService> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CategoryForView> GetAllCategoriesAsync()
        {
            try
            {
                IEnumerable<Category> categories = await _categoryRepository.GetAllCategorys();
                IList<CategoryForViewItems> items = _mapper.Map<IEnumerable<CategoryForViewItems>>(categories).ToList();
                CategoryForView result = new CategoryForView();
                result.Categories = items;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(GetAllCategoriesAsync));
            }
            return null!;
        }
    }
}

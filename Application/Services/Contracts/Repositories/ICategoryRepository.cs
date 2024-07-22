using Application.Services.Contracts.Repositories.Base;
using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;
using Domain.Entities;

namespace Application.Services.Contracts.Repositories
{
    public interface ICategoryRepository : IGeneralRepository<Category>
    {
        Task<Category> CreateCategoryAsync(Category category);
        Category UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<bool> IsUniqueCategoryName(string name);
        Task<IEnumerable<Category>> GetAllCategories();
        IQueryable<Category> GetAllCategories(SearchBaseModel model, CancellationToken cancellationToken);
    }
}

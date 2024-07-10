using Application.Services.Contracts.Repositories.Base;
using Domain.Entities;

namespace Application.Services.Contracts.Repositories
{
    public interface ICategoryRepository : IGeneralRepository<Category>
    {
        Task<Category> CreateCategoryAsync(Category category);
        Category UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategorys();
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<bool> IsUniqueCategoryName(string name);
    }
}

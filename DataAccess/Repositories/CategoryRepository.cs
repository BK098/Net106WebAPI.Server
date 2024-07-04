using Application.Services.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repositories.Base;

namespace Repositories.Repositories
{
    public class CategoryRepository : GeneralReposotory<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }
        #region Command
        public async Task<Category> CreateCategoryAsync(Category product)
        {
            return await CreateAsync(product);
        }
        public bool DeleteCategory(Category product)
        {
            Delete(product);
            return true;
        }
        public Category UpdateCategory(Category product)
        {
            return Update(product);
        }
        #endregion

        #region Queries
        public async Task<bool> IsUniqueCategoryName(string name, CancellationToken cancellationToken)
        {
            var product = await _context.Categories
                .FirstOrDefaultAsync(p => p.Name == name);

            return product == null;
        }
        public async Task<IEnumerable<Category>> GetAllCategorys()
        {
            return await _context.Categories
                .ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories
                .Include(p => p.Products)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
        #endregion
    }
}

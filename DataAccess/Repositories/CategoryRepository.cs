using Application.Services.Contracts.Repositories;
using Application.Services.Models.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repositories.Base;
using System.Linq.Expressions;

namespace Repositories.Repositories
{
    public class CategoryRepository : GeneralReposotory<Category>, ICategoryRepository
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
        public async Task<bool> IsUniqueCategoryName(string name)
        {
            var product = await _context.Categories
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

            return product == null;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories
                .Include(p => p.Products)
                .SingleOrDefaultAsync(p => p.Id == id && !p.IsDeleted.HasValue || !p.IsDeleted.Value);
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public IQueryable<Category> GetAllCategories(SearchBaseModel model, CancellationToken cancellationToken)
        {
            var categoryQuery = _context.Categories
                .Where(c => !c.IsDeleted.HasValue || !c.IsDeleted.Value)
                .Include(x => x.Products)                
                .AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                categoryQuery = categoryQuery.Where(p => 
                    p.Name.ToLower().Contains(model.SearchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(model.SortColumn))
            {
                var sortExpression = GetSortProperty(model.SortColumn);
                if (sortExpression != null)
                {
                    categoryQuery = model.SortOrder?.ToLower() == "desc"
                        ? categoryQuery.OrderByDescending(sortExpression)
                        : categoryQuery.OrderBy(sortExpression);
                }
            }

            return categoryQuery;
        }
        private static Expression<Func<Category, object>> GetSortProperty(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "name" => category => category.Name,
                _ => category => category.Id
            };
        }
        #endregion
    }
}

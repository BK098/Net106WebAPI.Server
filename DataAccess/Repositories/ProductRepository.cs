using Repositories.Repositories.Base;
using Application.Services.Contracts.Repositories;
using Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Application.Services.Models.Base;

namespace Repositories.Repositories
{
    public class ProductRepository : GeneralReposotory<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }
        #region Command
        public async Task<Product> CreateProductAsync(Product product)
        {
            return await CreateAsync(product);
        }
        public bool DeleteProduct(Product product)
        {
            Delete(product);
            return true;
        }
        public Product UpdateProduct(Product product)
        {
            return Update(product);
        }
        #endregion
        #region Queries
        public async Task<bool> IsUniqueProductName(string name)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

            return product == null;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .ToListAsync();
        }
        public IQueryable<Product> GetAllProducts(SearchBaseModel model, CancellationToken cancellationToken)
        {
            var productQuery = _context.Products
                .Include(c => c.Category)
                .Include(c => c.Images)
                .AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                productQuery = productQuery.Where(p =>
                    p.Name.ToLower().Contains(model.SearchTerm.ToLower()) ||
                    p.Category.Name.ToLower().Contains(model.SearchTerm.ToLower()));
            }

            if (model.SortOrder?.ToLower() == "desc")
            {
                productQuery = productQuery.OrderByDescending(GetSortProperty(model.SortColumn));
            }
            else
            {
                productQuery = productQuery.OrderBy(GetSortProperty(model.SortColumn));
            }

            return productQuery;
        }
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
        private static Expression<Func<Product, object>> GetSortProperty(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "name" => product => product.Name,
                "price" => product => product.Price,
                "discount" => product => product.Discount,
                "quantity" => product => product.StockQuantity,
                "category" => product => product.Category.Name,
                _ => product => product.Id
            };
        }
        #endregion
    }
}
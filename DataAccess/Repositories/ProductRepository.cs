using Repositories.Repositories.Base;
using Application.Services.Contracts.Repositories;
using Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        public async Task<bool> IsUniqueProductName(string name, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Name == name);

            return product == null;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
        #endregion
    }
}
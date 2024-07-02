using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repositories.Base;

namespace Repositories.Repositories
{
    public class ProductRepository: GeneralReposotory<Product>
    {
        public ProductRepository(ApplicationDbContext context): base(context)
        {
            
        }
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

using Application.Services.Contracts.Repositories.Base;
using Domain.Entities;

namespace Application.Services.Contracts.Repositories
{
    public interface IProductRepository: IGeneralRepository<Product>
    {
        Task<Product> CreateProductAsync(Product product);
        Product UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<bool> IsUniqueProductName(string name, CancellationToken cancellationToken);
    }
}

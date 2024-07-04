using Application.Services.Models.ProductModels;

namespace Application.Services.Contracts.Services.Queries
{
    public interface IProductQueryService
    {
        Task<ProductForView> GetAllProductsAsync();
        Task<ProductForViewItems> GetProductByIdAsync(Guid id);
    }
}

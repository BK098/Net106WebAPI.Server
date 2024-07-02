using Domain.Entities;

namespace Application.Services.Contracts.Repositories.Base
{
    public interface IUnitOfWork
    {
        IGeneralRepository<Product> ProductReposotory { get; }
        IGeneralRepository<Combo> ComboReposotory { get; }
        IGeneralRepository<Order> OrderReposotory { get; }
        IGeneralRepository<Image> ImageReposotory { get; }
        IGeneralRepository<ProductItem> ProductItemReposotory { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}

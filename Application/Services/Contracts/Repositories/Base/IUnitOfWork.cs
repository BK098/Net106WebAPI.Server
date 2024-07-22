namespace Application.Services.Contracts.Repositories.Base
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        IComboRepository Combo { get; }
        IOrderRepository Order { get; }
        ICategoryRepository Category { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}

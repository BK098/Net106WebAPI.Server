namespace Application.Services.Contracts.Repositories.Base
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}

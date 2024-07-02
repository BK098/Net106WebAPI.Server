using System.Linq.Expressions;

namespace Application.Services.Contracts.Repositories.Base
{
    public interface IGeneralRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}

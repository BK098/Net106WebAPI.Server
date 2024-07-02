using Application.Services.Contracts.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Repositories.Repositories.Base
{
    public class GeneralReposotory<TEntity>: IGeneralRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _entity;
        public GeneralReposotory(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            _context.Add(entity);
            return entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
            return entity;
        }

        public bool Delete(TEntity entity)
        {
            _entity.Remove(entity);
            return true;
        }

        public TEntity Update(TEntity entity)
        {
            _entity.Update(entity);
            return entity;
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

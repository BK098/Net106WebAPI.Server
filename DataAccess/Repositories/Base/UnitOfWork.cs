
using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Repositories.Base;
using Domain.Entities;
using Persistence;

namespace Repositories.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Product { get; }
        public UnitOfWork(ApplicationDbContext context, 
            IProductRepository product)
        {
            _context = context;
            Product = product;
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

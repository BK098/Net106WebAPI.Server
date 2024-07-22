using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Repositories.Base;
using Persistence;

namespace Repositories.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IComboRepository Combo { get; }
        public IProductRepository Product { get; }
        public IOrderRepository Order { get; }
        public ICategoryRepository Category { get; }
        public UnitOfWork(ApplicationDbContext context,
            IProductRepository product,
            IComboRepository combo,
            IOrderRepository order,
            ICategoryRepository category)
        {
            _context = context;
            Product = product;
            Combo = combo;
            Order = order;
            Category = category;
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

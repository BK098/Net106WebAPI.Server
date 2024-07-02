
using Application.Services.Contracts.Repositories.Base;
using Domain.Entities;
using Persistence;

namespace Repositories.Repositories.Base
{
    public class UnitOfWork //: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        /*public IReposotory ProductReposotory { get; }
        public IReposotory ComboReposotory { get; }
        public IReposotory OrderReposotory { get; }
        public IReposotory ImageReposotory { get; }
        public IReposotory ProductItemReposotory { get; }

        public UnitOfWork(ApplicationDbContext context,
            IReposotory productReposotory,
            IReposotory comboReposotory,
            IReposotory orderReposotory,
            IReposotory imageReposotory,
            IReposotory productItemReposotory)
        {
            _context = context;
            ProductReposotory = productReposotory;
            ComboReposotory = comboReposotory;
            OrderReposotory = orderReposotory;
            ImageReposotory = imageReposotory;
            ProductItemReposotory = productItemReposotory;
        }*/
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

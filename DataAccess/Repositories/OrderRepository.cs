using Application.Services.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repositories.Base;

namespace Repositories.Repositories
{
    public class OrderRepository : GeneralReposotory<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }
        #region Command
        public async Task<Order> CreateOrderAsync(Order order)
        {
            return await CreateAsync(order);
        }
        public async Task<Order> UpdateOrder(Order order)
        {
            return Update(order);
        }
        #endregion
        #region Queries
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(p => p.OrderItems)
                    .ThenInclude(c => c.Combo)
                .Include(p => p.OrderItems)
                    .ThenInclude(c => c.Product)
                .ToListAsync();
        }
        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(p => p.OrderItems)
                    .ThenInclude(p => p.Product)
                .Include(p => p.OrderItems)
                    .ThenInclude(c => c.Combo)
                 .SingleOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> IsComboOrProductOrderExist( Guid? comboId, Guid? productId)
        {
            return await _context.Orders
            .AnyAsync(o =>
                o.OrderItems.Any(oi => oi.ComboId == comboId) ||
                o.OrderItems.Any(oi => oi.ProductId == productId));
        }
        #endregion
    }
}

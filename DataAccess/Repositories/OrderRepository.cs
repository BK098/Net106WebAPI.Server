using Application.Services.Contracts.Repositories;
using Application.Services.Models.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repositories.Base;
using System.Linq.Expressions;

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
        public async Task<bool> IsComboOrProductOrderExist(Guid? comboId, Guid? productId)
        {
            return await _context.Orders
            .AnyAsync(o =>
                o.OrderItems.Any(oi => oi.ComboId == comboId) ||
                o.OrderItems.Any(oi => oi.ProductId == productId));
        }
        public IQueryable<Order> GetAllOrders(SearchBaseModel model, CancellationToken cancellationToken)
        {
            var orderQuery = _context.Orders
                .Include(u => u.User)
                .Include(u => u.OrderItems).ThenInclude(u => u.Product)
                .Include(u => u.OrderItems).ThenInclude(u => u.Combo)
                .AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(model.SearchTerm))    
            {
                orderQuery = orderQuery.Where(p =>
                    p.User.FirstName.ToLower().Contains(model.SearchTerm.ToLower())||
                    p.User.LastName.ToLower().Contains(model.SearchTerm.ToLower()) ||
                    p.Status.ToString().Contains(model.SearchTerm.ToLower()));
            }

            if (model.SortOrder?.ToLower() == "desc")
            {
                orderQuery = orderQuery.OrderByDescending(GetSortProperty(model.SortColumn));
            }
            else
            {
                orderQuery = orderQuery.OrderBy(GetSortProperty(model.SortColumn));
            }

            return orderQuery;
        }
        public IQueryable<Order> GetAllOrdersHitory(SearchBaseModel model, string userId, CancellationToken cancellationToken)
        {
            var orderQuery = _context.Orders
               .Include(u => u.User)
               .Include(u => u.OrderItems).ThenInclude(u => u.Product)
               .Include(u => u.OrderItems).ThenInclude(u => u.Combo)
               .Where(x => x.UserId == userId)
               .AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                orderQuery = orderQuery.Where(p =>
                    p.User.FirstName.ToLower().Contains(model.SearchTerm.ToLower()) ||
                    p.User.LastName.ToLower().Contains(model.SearchTerm.ToLower()) ||
                    p.Status.ToString().Contains(model.SearchTerm.ToLower()));
            }

            if (model.SortOrder?.ToLower() == "desc")
            {
                orderQuery = orderQuery.OrderByDescending(GetSortProperty(model.SortColumn));
            }
            else
            {
                orderQuery = orderQuery.OrderBy(GetSortProperty(model.SortColumn));
            }

            return orderQuery;
        }
        #endregion
        private static Expression<Func<Order, object>> GetSortProperty(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "firstname" => order => order.User.FirstName,
                "lastname" => order => order.User.LastName,
                "status" => order => order.Status,
                "amount" => order => order.TotalAmount,
                _ => order => order.Id
            };
        }
    }
}

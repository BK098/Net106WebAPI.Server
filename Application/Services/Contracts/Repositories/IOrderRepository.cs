using Application.Services.Contracts.Repositories.Base;
using Domain.Entities;

namespace Application.Services.Contracts.Repositories
{
    public interface IOrderRepository : IGeneralRepository<Order>
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
        //Task<bool> IsComboOrProductOrderExist(Guid orderId, Guid? comboId, Guid? productId);
        Task<bool> IsComboOrProductOrderExist(Guid? comboId, Guid? productId);

    }
}

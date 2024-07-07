using Application.Services.Contracts.Repositories.Base;
using Domain.Entities;

namespace Application.Services.Contracts.Repositories
{
    public interface IComboRepository : IGeneralRepository<Combo>
    {
        Task<Combo> CreateComboAsync(Combo combo);
        Combo UpdateCombo(Combo combo);
        bool DeleteCombo(Combo combo);
        Task<IEnumerable<Combo>> GetAllCombosAsync();
        Task<Combo> GetComboByIdAsync(Guid id);
        Task<bool> IsUniqueComboName(string name, CancellationToken cancellationToken);
        Task<bool> IsProductItemExist(Guid comboId,Guid productId, CancellationToken cancellationToken);
        
    }
}

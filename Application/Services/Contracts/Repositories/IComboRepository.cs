using Application.Services.Contracts.Repositories.Base;
using Application.Services.Models.Base;
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
        Task<bool> IsUniqueComboName(string name);
        Task<bool> IsProductComboExist(Guid comboId,Guid? productId);
        IQueryable<Combo> GetAllCombosAsync(SearchBaseModel model, CancellationToken cancellationToken);
    }
}

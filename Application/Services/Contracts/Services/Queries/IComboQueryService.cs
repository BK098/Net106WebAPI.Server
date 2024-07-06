using Application.Services.Models.ComboModels;

namespace Application.Services.Contracts.Services.Queries
{
    public interface IComboQueryService
    {
        Task<ComboForView> GetAllCombosAsync();
        Task<ComboForViewItems> GetComboByIdAsync(Guid id);
    }
}

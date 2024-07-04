using Application.Services.Models.CategoryModels;

namespace Application.Services.Contracts.Services.Queries
{
    public interface ICategoryQueryService
    {
        Task<CategoryForView> GetAllCategoriesAsync();
    }
}

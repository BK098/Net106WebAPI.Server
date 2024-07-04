using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;

namespace Application.Services.Contracts.Services.Commands
{
    public interface ICategoryCommandService
    {
        Task<UserMangeResponse> CreateCategoryAsync(CategoryForCreate CategoryDto);
    }
}

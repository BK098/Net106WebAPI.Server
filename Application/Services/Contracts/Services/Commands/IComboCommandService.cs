using Application.Services.Models.Base;
using Application.Services.Models.CategoryModels;
using Application.Services.Models.ComboModels;

namespace Application.Services.Contracts.Services.Commands
{
    public interface IComboCommandService
    {
        Task<UserMangeResponse> CreateCategoryAsync(ComboForCreate ComboDto);
    }
}

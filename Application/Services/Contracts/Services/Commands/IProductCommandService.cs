using Application.Services.Models.Base;
using Application.Services.Models.ProductModels;

namespace Application.Services.Contracts.Services.Commands
{
    public interface IProductCommandService
    {
        Task<UserMangeResponse> CreateProductAsync(ProductForCreate productDto);
    }
}

using Application.Services.Contracts.Repositories;
using Application.Services.Models.ProductModels.Base;
using FluentValidation;

namespace Application.Services.Models.ProductModels
{
    public class ProductForCreate : ProductBaseDto { }
    public class ProductForCreateValidator : AbstractValidator<ProductForCreate>
    {
        private readonly IProductRepository _productRepo;
        public ProductForCreateValidator(IProductRepository productRepo)
        {
            _productRepo = productRepo;
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage(x => $"Giá sản phẩm '{x.Price}' không  được bé hơn 0");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(x => $"Tên sản phẩm '{x.Name}' Bắt buộc phải có")
                .NotNull().WithMessage(x => $"Tên sản phẩm '{x.Name}' Không được để trống")
                .MustAsync(productRepo.IsUniqueProductName).WithMessage(x => $"Tên sản phẩm '{x.Name}' đã tồn tại");
        }
    }
}
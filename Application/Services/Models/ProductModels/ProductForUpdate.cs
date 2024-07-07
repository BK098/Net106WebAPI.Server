using Application.Services.Contracts.Repositories;
using Application.Services.Models.ProductModels.Base;
using FluentValidation;

namespace Application.Services.Models.ProductModels
{
    public class ProductForUpdate : ProductBaseDto { }
    public class ProductForUpdateValidator : AbstractValidator<ProductForUpdate>
    {
        private readonly IProductRepository _productRepo;
        public ProductForUpdateValidator(IProductRepository productRepo)
        {
            _productRepo = productRepo;
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(1).WithMessage(x => $"Giá sản phẩm '{x.Price}' không  được bé hơn 1");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(x => $"Tên sản phẩm '{x.Name}' Bắt buộc phải có")
                .NotNull().WithMessage(x => $"Tên sản phẩm '{x.Name}' Không được để trống")
                .MustAsync(productRepo.IsUniqueProductName).WithMessage(x => $"Tên sản phẩm '{x.Name}' đã tồn tại");
            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage(x => $"Số Giảm Giá '{x.Discount}' phải lớn hơn hoặc bằng 0");
            RuleFor(x => x.StockQuantity)
                .GreaterThan(0).WithMessage(x => $"số Lượng Phải lớn hơn 0 ");
        }
    }
}

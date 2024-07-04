using Application.Services.Contracts.Repositories;
using Application.Services.Models.ProductModels.Base;
using FluentValidation;

namespace Application.Services.Models.ProductModels
{
    public class ProductForCreate : ProdcutBaseDto
    {
        public Dictionary<string, object> Errors { get; set; } = new Dictionary<string, object>();
    }
    public class ProductForCreateValidator : AbstractValidator<ProductForCreate>
    {
        private readonly IProductRepository _productRepo;
        public ProductForCreateValidator(IProductRepository productRepo)
        {
            _productRepo = productRepo;

            //RuleFor(x => x.Price).NotEqual(0).WithMessage("Không được bằng 0");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Không được bé hơn 0");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Bắt buộc phải có");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Bắt buộc phải có")
                .NotNull().WithMessage("Không được để trống")
                .MustAsync(productRepo.IsUniqueProductName).WithMessage("Tên sản phẩm đã tồn tại");
        }
    }
}

using Application.Services.Contracts.Repositories;
using Application.Services.Models.ProductModels.Base;
using FluentValidation;

namespace Application.Services.Models.ProductModels
{
    public class ProductForCreate : ProductBaseDto { }
    public class ProductForCreateValidator : AbstractValidator<ProductForCreate>
    {
        public ProductForCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage($"Bắt buộc phải có")
                .NotNull().WithMessage($"Không được để trống");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(1).WithMessage($"Không được bé hơn hoặc bằng 0");
           // RuleFor(x => x.Discount)
                 //.GreaterThan(0).WithMessage($"Phải lớn hơn 0")
                //.GreaterThanOrEqualTo(0).WithMessage($"Không được bé 0");
            RuleFor(x => x.StockQuantity)
               .GreaterThan(0).WithMessage($"Phải lớn hơn 0");
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage($"Bắt buộc phải có")
                .NotNull().WithMessage($"Không được để trống");
        }
    }
}
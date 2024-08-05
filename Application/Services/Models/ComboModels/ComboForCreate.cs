using Application.Services.Models.ComboModels.Base;
using FluentValidation;

namespace Application.Services.Models.ComboModels
{

    public class ComboForCreate  : ComboBaseDto
    {
       public IList<ProductComboForCreate> ProductCombos { get; set; } = new List<ProductComboForCreate>();
    }
    public class ComboForCreateValidator : AbstractValidator<ComboForCreate>
    {
        public ComboForCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Không được để trống")
                .NotEmpty().WithMessage("Bắt buộc phải có");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage($"Phải lớn hơn 0")  
                .GreaterThan(0).WithMessage(x => $"Không được bé hơn 0");
            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Phải lớn hơn hoặc bằng 0");
            RuleForEach(x => x.ProductCombos).SetValidator(new ProductComboForCreateValidator());
        }
    }
    public class ProductComboForCreate
    {
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
    public class ProductComboForCreateValidator : AbstractValidator<ProductComboForCreate>
    {
        public ProductComboForCreateValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Bắt buộc phải có");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage($"Không được để trống")
                .GreaterThanOrEqualTo(1).WithMessage($"Phải lớn hơn hoặc bằng 1");
        }
    }
}

using Application.Services.Models.ComboModels.Base;
using FluentValidation;
using System.Text.Json.Serialization;

namespace Application.Services.Models.ComboModels
{
    public class ComboForUpdate : ComboBaseDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public bool? IsDeleted { get; set; }
        public IList<ProductComboForUpdate> ProductCombos { get; set; } = new List<ProductComboForUpdate>();
    }

    public class ComboForUpdateValidator : AbstractValidator<ComboForUpdate>
    {
        public ComboForUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Tên Combo không được để trống")
                .NotEmpty().WithMessage("Tên Combo là bắt buộc");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Hình Ảnh là bắt buộc");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Giá Combo không được để trống")
                .NotEmpty().WithMessage("Giá Combo cần phải có")
                .GreaterThanOrEqualTo(1).WithMessage("Giá Combo phải lớn hơn hoặc bằng 1");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Phải lớn hơn hoặc bằng 0")
            .GreaterThan(0).WithMessage($"Phải lớn hơn 0");
            RuleForEach(x => x.ProductCombos).SetValidator(new ProductComboForUpdateValidator());
        }
    }

    public class ProductComboForUpdate
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

    }

    public class ProductComboForUpdateValidator : AbstractValidator<ProductComboForUpdate>
    {
        public ProductComboForUpdateValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Không được để trống")
                .NotEmpty().WithMessage("Bắt buộc phải có");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1).WithMessage("Số lượng sản phẩm phải lớn hơn hoặc bằng 1");
        }
    }
}

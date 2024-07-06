using Application.Services.Contracts.Repositories;
using Application.Services.Models.ComboModels;
using Application.Services.Models.ComboModels.Base;
using FluentValidation;

namespace Application.Services.Models.ComboModels
{

    public class ComboForCreate  : ComboBaseDto
    {
       public List<ProductComboInforCreate> ProductCombos { get; set; } = new List<ProductComboInforCreate>();
    
    }
    public class ComboForCreateValidator : AbstractValidator<ComboForCreate>
    {
        private readonly IComboRepository _categoryRepository;
        public ComboForCreateValidator(IComboRepository categoryRepo)
        {
            _categoryRepository = categoryRepo;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Bắt buộc phải có")
                .NotNull().WithMessage("Không được để trống")
                .MustAsync(categoryRepo.IsUniqueComboName).WithMessage("Combo này đã tồn tại");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage(x => $"Giá sản phẩm '{x.Price}' không  được bé hơn 0");
            RuleFor(x => x.Discount)
                .GreaterThan(0).WithMessage("Phải lớn hơn 0")
                .LessThanOrEqualTo(100).WithMessage("Phải nhỏ hơn hoặc bằng 100");

        }
    }
    public class ProductComboInforCreate
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

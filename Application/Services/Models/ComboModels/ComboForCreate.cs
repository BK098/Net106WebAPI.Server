using Application.Services.Contracts.Repositories;
using Application.Services.Models.ComboModels;
using Application.Services.Models.ComboModels.Base;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Services.Models.ComboModels
{

    public class ComboForCreate  : ComboBaseDto
    {
       public List<ProductComboInforCreate> ProductCombos { get; set; } = new List<ProductComboInforCreate>();
    
    }
    public class ComboForCreateValidator : AbstractValidator<ComboForCreate>
    {
        private readonly IComboRepository _comboRepository;
        public ComboForCreateValidator(IComboRepository comboRepo)
        {
            _comboRepository = comboRepo;

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Không được để trống")
                .NotEmpty().WithMessage("Bắt buộc phải có")
                .MustAsync(comboRepo.IsUniqueComboName).WithMessage("Combo này đã tồn tại");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage(x => $"Giá sản phẩm '{x.Price}' không  được bé hơn 0");
            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Phải lớn hơn hoặc bằng 0")
                .LessThanOrEqualTo(100).WithMessage("Phải nhỏ hơn hoặc bằng 100");
            RuleForEach(x => x.ProductCombos).SetValidator(new ProductComboInforCreateValidator(_comboRepository));
        }
    }
    public class ProductComboInforCreate
    {
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
    public class ProductComboInforCreateValidator : AbstractValidator<ProductComboInforCreate>
    {
        private readonly IComboRepository _comboRepository;
        public ProductComboInforCreateValidator(IComboRepository comboRepository)
        {
            _comboRepository = comboRepository;

           RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage(x => $"Mã sản phẩm bắt buộc phải có")
                .MustAsync((combo, productId, cancellationToken) => comboRepository.IsProductItemExist(Guid.NewGuid(), productId.Value, cancellationToken))
                .WithMessage(x => $"Mã sản phẩm '{x.ProductId}' đã tồn tại trong combo");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage(x => $"Số lượng '{x.Quantity}' không được để trống")
                .GreaterThanOrEqualTo(0).WithMessage(x => $"Số lượng '{x.Quantity}' phải lớn hơn hoặc bằng 0");
        }
    }
}

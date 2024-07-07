using Application.Services.Contracts.Repositories;
using Application.Services.Models.ComboModels.Base;
using FluentValidation;

namespace Application.Services.Models.ComboModels
{
    public class ComboForUpdate : ComboBaseDto
    {
        public List<ProductItemInfoForUpdate> ComboItems { get; set; } = new List<ProductItemInfoForUpdate>();
    }

    public class ComboForUpdateValidator : AbstractValidator<ComboForUpdate>
    {
        private readonly IComboRepository _comboRepository;

        public ComboForUpdateValidator(IComboRepository comboRepository)
        {
            _comboRepository = comboRepository;

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
                .LessThanOrEqualTo(100).WithMessage("Phải nhỏ hơn hoặc bằng 100");

            RuleForEach(x => x.ComboItems).SetValidator(new ProductItemInfoForUpdateValidator(_comboRepository));
        }
    }

    public class ProductItemInfoForUpdate
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductItemInfoForUpdateValidator : AbstractValidator<ProductItemInfoForUpdate>
    {
        private readonly IComboRepository _comboRepository;

        public ProductItemInfoForUpdateValidator(IComboRepository comboRepository)
        {
            _comboRepository = comboRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Mã sản phẩm không được để trống")
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _comboRepository.IsProductItemExist(Guid.NewGuid(), id, cancellationToken);
                }).WithMessage("Mã sản phẩm không tồn tại trong combo");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Số lượng sản phẩm phải lớn hơn 0");
        }
    }
}

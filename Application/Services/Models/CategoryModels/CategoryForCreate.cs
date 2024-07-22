using Application.Services.Models.CategoryModels.Base;
using FluentValidation;

namespace Application.Services.Models.CategoryModels
{
    public class CategoryForCreate : CategoryBaseDto
    {
    }
    public class CategoryForCreateValidator : AbstractValidator<CategoryForCreate>
    {
        public CategoryForCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Bắt buộc phải có")
                .NotNull().WithMessage("Không được để trống");
        }
    }
}

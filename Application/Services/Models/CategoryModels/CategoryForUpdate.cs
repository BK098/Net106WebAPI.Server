using Application.Services.Contracts.Repositories;
using Application.Services.Models.CategoryModels.Base;
using Application.Services.Models.CategoryModels;
using FluentValidation;

namespace Application.Services.Models.CategoryModels
{
    public class CategoryForUpdate : CategoryBaseDto { }
    public class CategoryForUpdateValidator : AbstractValidator<CategoryForUpdate>
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryForUpdateValidator(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(x => $"Tên sản phẩm '{x.Name}' Bắt buộc phải có")
                .NotNull().WithMessage(x => $"Tên sản phẩm '{x.Name}' Không được để trống")
                .MustAsync(categoryRepo.IsUniqueCategoryName).WithMessage(x => $"Tên sản phẩm '{x.Name}' đã tồn tại");
        }
    }
}

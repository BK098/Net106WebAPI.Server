using Application.Services.Contracts.Repositories;
using Application.Services.Models.CategoryModels.Base;
using FluentValidation;

namespace Application.Services.Models.CategoryModels
{
    public class CategoryForCreate : CategoryBaseDto
    {
        public Dictionary<string, object> Errors { get; set; } = new Dictionary<string, object>();
    }
    public class CategoryForCreateValidator : AbstractValidator<CategoryForCreate>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryForCreateValidator(ICategoryRepository categoryRepo)
        {
            _categoryRepository = categoryRepo;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Bắt buộc phải có")
                .NotNull().WithMessage("Không được để trống")
                .MustAsync(categoryRepo.IsUniqueCategoryName).WithMessage("Thể loại này đã tồn tại");
        }
    }
}

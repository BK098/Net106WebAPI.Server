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
        private readonly ICategoryRepository _categoryRepo;
        public CategoryForCreateValidator(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;

            //RuleFor(x => x.Price).NotEqual(0).WithMessage("Không được bằng 0");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Bắt buộc phải có")
                .NotNull().WithMessage("Không được để trống")
                .MustAsync(categoryRepo.IsUniqueCategoryName).WithMessage("Thể loại này đã tồn tại");
        }
    }
}

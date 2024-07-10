using Application.Services.Models.CategoryModels.Base;
using FluentValidation;
using System.Text.Json.Serialization;

namespace Application.Services.Models.CategoryModels
{
    public class CategoryForUpdate : CategoryBaseDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class CategoryForUpdateValidator : AbstractValidator<CategoryForUpdate>
    {
        public CategoryForUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Bắt buộc phải có")
                .NotNull().WithMessage($"Không được để trống");
        }
    }
}
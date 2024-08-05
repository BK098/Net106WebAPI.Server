using Application.Services.Models.CategoryModels.Base;

namespace Application.Services.Models.CategoryModels
{
    public class CategoryForView : CategoryBaseDto
    {
        public Guid Id { get; set; }
        public int ProductCount { get; set; }
        public bool IsDeleted { get; set; } // Thêm trường này

    }
}
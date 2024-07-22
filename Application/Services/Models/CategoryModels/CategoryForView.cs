using Application.Services.Models.CategoryModels.Base;

namespace Application.Services.Models.CategoryModels
{
    public class CategoryForView : CategoryBaseDto
    {
        public Guid Id { get; set; }
    }
}
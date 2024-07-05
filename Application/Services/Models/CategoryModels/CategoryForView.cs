using Application.Services.Models.CategoryModels.Base;

namespace Application.Services.Models.CategoryModels
{
    public class CategoryForView
    {
        public IList<CategoryForViewItems> Categories { get; set; } = new List<CategoryForViewItems>();
    }
    public class CategoryForViewItems : CategoryBaseDto
    {
        public Guid Id { get; set; }
    }
}
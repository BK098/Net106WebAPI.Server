using Application.Services.Models.ProductModels.Base;

namespace Application.Services.Models.ProductModels
{
    public class ProductForView: ProductBaseDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; } // Thêm trường này

    }
}
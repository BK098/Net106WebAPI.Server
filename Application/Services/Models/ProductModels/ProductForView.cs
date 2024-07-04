using Application.Services.Models.ProductModels.Base;

namespace Application.Services.Models.ProductModels
{
    public class ProductForView
    {
        public IList<ProductForViewItems> Products { get; set; } = new List<ProductForViewItems>();
    }
    public class ProductForViewItems: ProductBaseDto
    {
        public Guid Id { get; set; }
    }
}

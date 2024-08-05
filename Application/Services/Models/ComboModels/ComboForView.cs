using Application.Services.Models.ComboModels.Base;

namespace Application.Services.Models.ComboModels
{
    public class ComboForView : ComboBaseDto
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } // Thêm trường này

        public IList<ProductComboForView> ProductCombos { get; set; } = new List<ProductComboForView>();
    }
    public class ProductComboForView
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; } //Product Combo

    }
}

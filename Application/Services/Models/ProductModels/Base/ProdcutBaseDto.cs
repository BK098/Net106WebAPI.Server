namespace Application.Services.Models.ProductModels.Base
{
    public class ProdcutBaseDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int Discount { get; set; }
        public int StockQuantity { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

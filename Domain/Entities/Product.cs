using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public int Discount { get; set; }
        public int StockQuantity { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset LastUpdated { get; set; }

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
        public Category? Category{ get; set; }

        public ICollection<Image>? Images { get; set; }
        public ICollection<ProductCombo>? ProductCombos { get; set; }
        public ICollection<ReceiptItem>? ReceiptItems { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<ProductEntry>? ProductEntries { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductEntry
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Suplier")]
        public Guid? SuplierId { get; set; }
        public Suplier? Suplier { get; set; }
    }
}

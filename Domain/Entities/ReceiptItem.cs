using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ReceiptItem
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }

        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Receipt")]
        public Guid? ReceiptId { get; set; }
        public Receipt? Receipt { get; set; }
    }
}

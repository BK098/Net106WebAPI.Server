using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }

        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Order")]
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        [ForeignKey("Combo")]
        public Guid? ComboId { get; set; }
        public Combo? Combo { get; set; }
    }
}

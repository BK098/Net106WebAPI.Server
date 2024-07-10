using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductCombo
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Combo")]
        public Guid? ComboId { get; set; }
        public Combo? Combo { get; set; }
    }
}

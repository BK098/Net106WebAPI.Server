using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Combo
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int Discount { get; set; }
        public double Price { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public ICollection<Image>? Images { get; set; }
        public ICollection<ProductCombo>? ProductCombos { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}

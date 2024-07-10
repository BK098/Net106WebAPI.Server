using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public ICollection<Product>? Products { get; set; }
    }
}
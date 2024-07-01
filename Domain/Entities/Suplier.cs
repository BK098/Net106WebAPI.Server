using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Suplier
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ContactInfo { get; set; }

        public ICollection<ProductEntry>? ProductEntries { get; set; }
    }
}

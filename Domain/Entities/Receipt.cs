using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Receipt
    {
        [Key]
        public Guid Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTimeOffset DateReceipt { get; set; }

        [ForeignKey("AppUser")]
        public string? UserId { get; set; }
        public AppUser? User { get; set; }

        public ICollection<ReceiptItem>? ReceiptItems { get; set; }
    }
}

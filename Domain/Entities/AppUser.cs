
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? ImagePath { get; set; }
        
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Receipt>? Receipts { get; set; }
    }
}
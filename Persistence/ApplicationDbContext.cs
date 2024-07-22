using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }*/
        public DbSet<Category> Categories { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductCombo> ProductCombos { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptItem> ReceiptItems { get; set; }
        public DbSet<Suplier> Supliers { get; set; }
        public DbSet<ProductEntry> ProductEntries { get; set; }
    }
}

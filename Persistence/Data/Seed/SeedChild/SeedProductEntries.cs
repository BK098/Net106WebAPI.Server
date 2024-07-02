using Domain.Entities;
using Persistence;

public class SeedProductEntries
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.ProductEntries.Any())
        {
            context.ProductEntries.AddRange(
                new ProductEntry { Quantity = 10, UnitPrice = 10, DateUpdated = DateTimeOffset.Now, ProductId = context.Products.First().Id, SuplierId = context.Supliers.First().Id },
                new ProductEntry { Quantity = 20, UnitPrice = 20, DateUpdated = DateTimeOffset.Now, ProductId = context.Products.Skip(1).First().Id, SuplierId = context.Supliers.Skip(1).First().Id },
                new ProductEntry { Quantity = 30, UnitPrice = 30, DateUpdated = DateTimeOffset.Now, ProductId = context.Products.Skip(2).First().Id, SuplierId = context.Supliers.Skip(2).First().Id },
                new ProductEntry { Quantity = 40, UnitPrice = 40, DateUpdated = DateTimeOffset.Now, ProductId = context.Products.Skip(3).First().Id, SuplierId = context.Supliers.Skip(3).First().Id },
                new ProductEntry { Quantity = 50, UnitPrice = 50, DateUpdated = DateTimeOffset.Now, ProductId = context.Products.Skip(4).First().Id, SuplierId = context.Supliers.Skip(4).First().Id }
            );
            context.SaveChanges();
        }
    }
}

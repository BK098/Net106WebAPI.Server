using Domain.Entities;
using Persistence;

public class SeedProducts
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Name = "Product1", Price = 100, DateAdded = DateTimeOffset.Now, LastUpdated = DateTimeOffset.Now },
                new Product { Name = "Product2", Price = 200, DateAdded = DateTimeOffset.Now, LastUpdated = DateTimeOffset.Now },
                new Product { Name = "Product3", Price = 300, DateAdded = DateTimeOffset.Now, LastUpdated = DateTimeOffset.Now },
                new Product { Name = "Product4", Price = 400, DateAdded = DateTimeOffset.Now, LastUpdated = DateTimeOffset.Now },
                new Product { Name = "Product5", Price = 500, DateAdded = DateTimeOffset.Now, LastUpdated = DateTimeOffset.Now }
            );
            context.SaveChanges();
        }
    }
}

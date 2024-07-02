using Domain.Entities;
using Persistence;

public class SeedProductItems
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.ProductItems.Any())
        {
            context.ProductItems.AddRange(
                new ProductItem { Quantity = 1, ProductId = context.Products.First().Id, ComboId = context.Combos.First().Id },
                new ProductItem { Quantity = 2, ProductId = context.Products.Skip(1).First().Id, ComboId = context.Combos.Skip(1).First().Id },
                new ProductItem { Quantity = 3, ProductId = context.Products.Skip(2).First().Id, ComboId = context.Combos.Skip(2).First().Id },
                new ProductItem { Quantity = 4, ProductId = context.Products.Skip(3).First().Id, ComboId = context.Combos.Skip(3).First().Id },
                new ProductItem { Quantity = 5, ProductId = context.Products.Skip(4).First().Id, ComboId = context.Combos.Skip(4).First().Id }
            );
            context.SaveChanges();
        }
    }
}

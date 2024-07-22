using Domain.Entities;
using Persistence;

public class SeedProductCombos
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.ProductCombos.Any())
        {
            context.ProductCombos.AddRange(
                new ProductCombo { Quantity = 1, ProductId = context.Products.First().Id, ComboId = context.Combos.First().Id },
                new ProductCombo { Quantity = 2, ProductId = context.Products.Skip(1).First().Id, ComboId = context.Combos.Skip(1).First().Id },
                new ProductCombo { Quantity = 3, ProductId = context.Products.Skip(2).First().Id, ComboId = context.Combos.Skip(2).First().Id },
                new ProductCombo { Quantity = 4, ProductId = context.Products.Skip(3).First().Id, ComboId = context.Combos.Skip(3).First().Id },
                new ProductCombo { Quantity = 5, ProductId = context.Products.Skip(4).First().Id, ComboId = context.Combos.Skip(4).First().Id }
            );
            context.SaveChanges();
        }
    }
}
using Domain.Entities;
using Persistence;

public class SeedCombos
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Combos.Any())
        {
            context.Combos.AddRange(
                new Combo { Name = "Combo1", Discount = 10, Price = 1000 },
                new Combo { Name = "Combo2", Discount = 20, Price = 2000 },
                new Combo { Name = "Combo3", Discount = 30, Price = 3000 },
                new Combo { Name = "Combo4", Discount = 40, Price = 4000 },
                new Combo { Name = "Combo5", Discount = 50, Price = 5000 }
            );
            context.SaveChanges();
        }
    }
}

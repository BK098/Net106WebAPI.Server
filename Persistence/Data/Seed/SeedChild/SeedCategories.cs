using Domain.Entities;
using Persistence;

public class SeedCategories
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Category1" },
                new Category { Name = "Category2" },
                new Category { Name = "Category3" },
                new Category { Name = "Category4" },
                new Category { Name = "Category5" }
            );
            context.SaveChanges();
        }
    }
}

using Domain.Entities;
using Persistence;

public class SeedSupliers
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Supliers.Any())
        {
            context.Supliers.AddRange(
                new Suplier { Name = "Suplier1", ContactInfo = "Contact1" },
                new Suplier { Name = "Suplier2", ContactInfo = "Contact2" },
                new Suplier { Name = "Suplier3", ContactInfo = "Contact3" },
                new Suplier { Name = "Suplier4", ContactInfo = "Contact4" },
                new Suplier { Name = "Suplier5", ContactInfo = "Contact5" }
            );
            context.SaveChanges();
        }
    }
}

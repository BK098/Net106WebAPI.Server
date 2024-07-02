using Domain.Entities;
using Persistence;

public class SeedReceipts
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Receipts.Any())
        {
            context.Receipts.AddRange(
                new Receipt { TotalAmount = 1000, DateReceipt = DateTimeOffset.Now, UserId = context.Users.First().Id },
                new Receipt { TotalAmount = 2000, DateReceipt = DateTimeOffset.Now, UserId = context.Users.Skip(1).First().Id },
                new Receipt { TotalAmount = 3000, DateReceipt = DateTimeOffset.Now, UserId = context.Users.Skip(2).First().Id },
                new Receipt { TotalAmount = 4000, DateReceipt = DateTimeOffset.Now, UserId = context.Users.Skip(3).First().Id },
                new Receipt { TotalAmount = 5000, DateReceipt = DateTimeOffset.Now, UserId = context.Users.Skip(4).First().Id }
            );
            context.SaveChanges();
        }
    }
}

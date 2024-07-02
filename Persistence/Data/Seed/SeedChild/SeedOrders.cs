using Domain.Entities;
using Persistence;

public class SeedOrders
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Orders.Any())
        {
            context.Orders.AddRange(
                new Order { OrderDate = DateTimeOffset.Now, TotalAmount = 1000, UserId = context.Users.First().Id },
                new Order { OrderDate = DateTimeOffset.Now, TotalAmount = 2000, UserId = context.Users.Skip(1).First().Id },
                new Order { OrderDate = DateTimeOffset.Now, TotalAmount = 3000, UserId = context.Users.Skip(2).First().Id },
                new Order { OrderDate = DateTimeOffset.Now, TotalAmount = 4000, UserId = context.Users.Skip(3).First().Id },
                new Order { OrderDate = DateTimeOffset.Now, TotalAmount = 5000, UserId = context.Users.Skip(4).First().Id }
            );
            context.SaveChanges();
        }
    }
}

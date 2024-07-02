using Domain.Entities;
using Persistence;

public class SeedOrderItems
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.OrderItems.Any())
        {
            context.OrderItems.AddRange(
                new OrderItem { Quantity = 1, UnitPrice = 100, TotalPrice = 100, ProductId = context.Products.First().Id, OrderId = context.Orders.First().Id },
                new OrderItem { Quantity = 2, UnitPrice = 200, TotalPrice = 400, ProductId = context.Products.Skip(1).First().Id, OrderId = context.Orders.Skip(1).First().Id },
                new OrderItem { Quantity = 3, UnitPrice = 300, TotalPrice = 900, ProductId = context.Products.Skip(2).First().Id, OrderId = context.Orders.Skip(2).First().Id },
                new OrderItem { Quantity = 4, UnitPrice = 400, TotalPrice = 1600, ProductId = context.Products.Skip(3).First().Id, OrderId = context.Orders.Skip(3).First().Id },
                new OrderItem { Quantity = 5, UnitPrice = 500, TotalPrice = 2500, ProductId = context.Products.Skip(4).First().Id, OrderId = context.Orders.Skip(4).First().Id }
            );
            context.SaveChanges();
        }
    }
}

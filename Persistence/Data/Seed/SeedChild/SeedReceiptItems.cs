using Domain.Entities;
using Persistence;

public class SeedReceiptItems
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.ReceiptItems.Any())
        {
            context.ReceiptItems.AddRange(
                new ReceiptItem { Quantity = 1, UnitPrice = 100, TotalPrice = 100, ProductId = context.Products.First().Id, ReceiptId = context.Receipts.First().Id },
                new ReceiptItem { Quantity = 2, UnitPrice = 200, TotalPrice = 400, ProductId = context.Products.Skip(1).First().Id, ReceiptId = context.Receipts.Skip(1).First().Id },
                new ReceiptItem { Quantity = 3, UnitPrice = 300, TotalPrice = 900, ProductId = context.Products.Skip(2).First().Id, ReceiptId = context.Receipts.Skip(2).First().Id },
                new ReceiptItem { Quantity = 4, UnitPrice = 400, TotalPrice = 1600, ProductId = context.Products.Skip(3).First().Id, ReceiptId = context.Receipts.Skip(3).First().Id },
                new ReceiptItem { Quantity = 5, UnitPrice = 500, TotalPrice = 2500, ProductId = context.Products.Skip(4).First().Id, ReceiptId = context.Receipts.Skip(4).First().Id }
            );
            context.SaveChanges();
        }
    }
}

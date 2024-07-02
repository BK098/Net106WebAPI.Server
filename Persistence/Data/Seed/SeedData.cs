using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            // Seed data for AppUser
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(), 
                        UserName = "user1",
                        NormalizedUserName = "USER1",
                        Email = "user1@example.com",
                        NormalizedEmail = "USER1@EXAMPLE.COM",
                        EmailConfirmed = true,
                        FirstName = "FirstName1",
                        LastName = "LastName1",
                        Address = "Address1",
                        ImagePath = "ImagePath1"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "user2",
                        NormalizedUserName = "USER2",
                        Email = "user2@example.com",
                        NormalizedEmail = "USER2@EXAMPLE.COM",
                        EmailConfirmed = true,
                        FirstName = "FirstName2",
                        LastName = "LastName2",
                        Address = "Address2",
                        ImagePath = "ImagePath2"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "user3",
                        NormalizedUserName = "USER3",
                        Email = "user3@example.com",
                        NormalizedEmail = "USER3@EXAMPLE.COM",
                        EmailConfirmed = true,
                        FirstName = "FirstName3",
                        LastName = "LastName3",
                        Address = "Address3",
                        ImagePath = "ImagePath3"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "user4",
                        NormalizedUserName = "USER4",
                        Email = "user4@example.com",
                        NormalizedEmail = "USER4@EXAMPLE.COM",
                        EmailConfirmed = true,
                        FirstName = "FirstName4",
                        LastName = "LastName4",
                        Address = "Address4",
                        ImagePath = "ImagePath4"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "user5",
                        NormalizedUserName = "USER5",
                        Email = "user5@example.com",
                        NormalizedEmail = "USER5@EXAMPLE.COM",
                        EmailConfirmed = true,
                        FirstName = "FirstName5",
                        LastName = "LastName5",
                        Address = "Address5",
                        ImagePath = "ImagePath5"
                    }
                );
                context.SaveChanges();
            }

            // Seed data for Category
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Id = Guid.NewGuid(), Name = "Category1" },
                    new Category { Id = Guid.NewGuid(), Name = "Category2" },
                    new Category { Id = Guid.NewGuid(), Name = "Category3" },
                    new Category { Id = Guid.NewGuid(), Name = "Category4" },
                    new Category { Id = Guid.NewGuid(), Name = "Category5" }
                );
                context.SaveChanges();
            }

            // Seed data for Product
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product1",
                        Price = 100,
                        Description = "Description1",
                        IsDeleted = false,
                        Discount = 10,
                        StockQuantity = 50,
                        DateAdded = DateTimeOffset.Now,
                        LastUpdated = DateTimeOffset.Now,
                        CategoryId = context.Categories.First().Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product2",
                        Price = 200,
                        Description = "Description2",
                        IsDeleted = false,
                        Discount = 20,
                        StockQuantity = 100,
                        DateAdded = DateTimeOffset.Now,
                        LastUpdated = DateTimeOffset.Now,
                        CategoryId = context.Categories.Skip(1).First().Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product3",
                        Price = 300,
                        Description = "Description3",
                        IsDeleted = false,
                        Discount = 30,
                        StockQuantity = 150,
                        DateAdded = DateTimeOffset.Now,
                        LastUpdated = DateTimeOffset.Now,
                        CategoryId = context.Categories.Skip(2).First().Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product4",
                        Price = 400,
                        Description = "Description4",
                        IsDeleted = false,
                        Discount = 40,
                        StockQuantity = 200,
                        DateAdded = DateTimeOffset.Now,
                        LastUpdated = DateTimeOffset.Now,
                        CategoryId = context.Categories.Skip(3).First().Id
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Product5",
                        Price = 500,
                        Description = "Description5",
                        IsDeleted = false,
                        Discount = 50,
                        StockQuantity = 250,
                        DateAdded = DateTimeOffset.Now,
                        LastUpdated = DateTimeOffset.Now,
                        CategoryId = context.Categories.Skip(4).First().Id
                    }
                );
                context.SaveChanges();
            }

            // Seed data for Combo
            if (!context.Combos.Any())
            {
                context.Combos.AddRange(
                    new Combo
                    {
                        Id = Guid.NewGuid(),
                        Name = "Combo1",
                        Description = "Description1",
                        Image = "Image1",
                        Discount = 10,
                        Price = 100,
                        IsDeleted = false
                    },
                    new Combo
                    {
                        Id = Guid.NewGuid(),
                        Name = "Combo2",
                        Description = "Description2",
                        Image = "Image2",
                        Discount = 20,
                        Price = 200,
                        IsDeleted = false
                    },
                    new Combo
                    {
                        Id = Guid.NewGuid(),
                        Name = "Combo3",
                        Description = "Description3",
                        Image = "Image3",
                        Discount = 30,
                        Price = 300,
                        IsDeleted = false
                    },
                    new Combo
                    {
                        Id = Guid.NewGuid(),
                        Name = "Combo4",
                        Description = "Description4",
                        Image = "Image4",
                        Discount = 40,
                        Price = 400,
                        IsDeleted = false
                    },
                    new Combo
                    {
                        Id = Guid.NewGuid(),
                        Name = "Combo5",
                        Description = "Description5",
                        Image = "Image5",
                        Discount = 50,
                        Price = 500,
                        IsDeleted = false
                    }
                );
                context.SaveChanges();
            }

            // Seed data for Image
            if (!context.Images.Any())
            {
                context.Images.AddRange(
                    new Image { Id = Guid.NewGuid(), ImagePath = "ImagePath1" },
                    new Image { Id = Guid.NewGuid(), ImagePath = "ImagePath2" },
                    new Image { Id = Guid.NewGuid(), ImagePath = "ImagePath3" },
                    new Image { Id = Guid.NewGuid(), ImagePath = "ImagePath4" },
                    new Image { Id = Guid.NewGuid(), ImagePath = "ImagePath5" }
                );
                context.SaveChanges();
            }

            // Seed data for Order
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order
                    {
                        Id = Guid.NewGuid(),
                        OrderDate = DateTimeOffset.Now,
                        TotalAmount = 1000,
                        UserId = context.Users.First().Id
                    },
                    new Order
                    {
                        Id = Guid.NewGuid(),
                        OrderDate = DateTimeOffset.Now,
                        TotalAmount = 2000,
                        UserId = context.Users.Skip(1).First().Id
                    },
                    new Order
                    {
                        Id = Guid.NewGuid(),
                        OrderDate = DateTimeOffset.Now,
                        TotalAmount = 3000,
                        UserId = context.Users.Skip(2).First().Id
                    },
                    new Order
                    {
                        Id = Guid.NewGuid(),
                        OrderDate = DateTimeOffset.Now,
                        TotalAmount = 4000,
                        UserId = context.Users.Skip(3).First().Id
                    },
                    new Order
                    {
                        Id = Guid.NewGuid(),
                        OrderDate = DateTimeOffset.Now,
                        TotalAmount = 5000,
                        UserId = context.Users.Skip(4).First().Id
                    }
                );
                context.SaveChanges();
            }

            // Seed data for OrderItem
            if (!context.OrderItems.Any())
            {
                context.OrderItems.AddRange(
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        UnitPrice = 100,
                        TotalPrice = 100,
                        ProductId = context.Products.First().Id,
                        OrderId = context.Orders.First().Id
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        UnitPrice = 200,
                        TotalPrice = 400,
                        ProductId = context.Products.Skip(1).First().Id,
                        OrderId = context.Orders.Skip(1).First().Id
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 3,
                        UnitPrice = 300,
                        TotalPrice = 900,
                        ProductId = context.Products.Skip(2).First().Id,
                        OrderId = context.Orders.Skip(2).First().Id
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 4,
                        UnitPrice = 400,
                        TotalPrice = 1600,
                        ProductId = context.Products.Skip(3).First().Id,
                        OrderId = context.Orders.Skip(3).First().Id
                    },
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 5,
                        UnitPrice = 500,
                        TotalPrice = 2500,
                        ProductId = context.Products.Skip(4).First().Id,
                        OrderId = context.Orders.Skip(4).First().Id
                    }
                );
                context.SaveChanges();
            }

            // Seed data for ProductEntry
            if (!context.ProductEntries.Any())
            {
                context.ProductEntries.AddRange(
                    new ProductEntry
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 10,
                        UnitPrice = 10,
                        DateUpdated = DateTimeOffset.Now,
                        ProductId = context.Products.First().Id,
                        SuplierId = context.Supliers.First().Id
                    },
                    new ProductEntry
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 20,
                        UnitPrice = 20,
                        DateUpdated = DateTimeOffset.Now,
                        ProductId = context.Products.Skip(1).First().Id,
                        SuplierId = context.Supliers.Skip(1).First().Id
                    },
                    new ProductEntry
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 30,
                        UnitPrice = 30,
                        DateUpdated = DateTimeOffset.Now,
                        ProductId = context.Products.Skip(2).First().Id,
                        SuplierId = context.Supliers.Skip(2).First().Id
                    },
                    new ProductEntry
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 40,
                        UnitPrice = 40,
                        DateUpdated = DateTimeOffset.Now,
                        ProductId = context.Products.Skip(3).First().Id,
                        SuplierId = context.Supliers.Skip(3).First().Id
                    },
                    new ProductEntry
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 50,
                        UnitPrice = 50,
                        DateUpdated = DateTimeOffset.Now,
                        ProductId = context.Products.Skip(4).First().Id,
                        SuplierId = context.Supliers.Skip(4).First().Id
                    }
                );
                context.SaveChanges();
            }

            // Seed data for ProductItem
            if (!context.ProductItems.Any())
            {
                context.ProductItems.AddRange(
                    new ProductItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        ProductId = context.Products.First().Id,
                        ComboId = context.Combos.First().Id
                    },
                    new ProductItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        ProductId = context.Products.Skip(1).First().Id,
                        ComboId = context.Combos.Skip(1).First().Id
                    },
                    new ProductItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 3,
                        ProductId = context.Products.Skip(2).First().Id,
                        ComboId = context.Combos.Skip(2).First().Id
                    },
                    new ProductItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 4,
                        ProductId = context.Products.Skip(3).First().Id,
                        ComboId = context.Combos.Skip(3).First().Id
                    },
                    new ProductItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 5,
                        ProductId = context.Products.Skip(4).First().Id,
                        ComboId = context.Combos.Skip(4).First().Id
                    }
                );
                context.SaveChanges();
            }

            // Seed data for Receipt
            if (!context.Receipts.Any())
            {
                context.Receipts.AddRange(
                    new Receipt
                    {
                        Id = Guid.NewGuid(),
                        TotalAmount = 1000,
                        DateReceipt = DateTimeOffset.Now,
                        UserId = context.Users.First().Id
                    },
                    new Receipt
                    {
                        Id = Guid.NewGuid(),
                        TotalAmount = 2000,
                        DateReceipt = DateTimeOffset.Now,
                        UserId = context.Users.Skip(1).First().Id
                    },
                    new Receipt
                    {
                        Id = Guid.NewGuid(),
                        TotalAmount = 3000,
                        DateReceipt = DateTimeOffset.Now,
                        UserId = context.Users.Skip(2).First().Id
                    },
                    new Receipt
                    {
                        Id = Guid.NewGuid(),
                        TotalAmount = 4000,
                        DateReceipt = DateTimeOffset.Now,
                        UserId = context.Users.Skip(3).First().Id
                    },
                    new Receipt
                    {
                        Id = Guid.NewGuid(),
                        TotalAmount = 5000,
                        DateReceipt = DateTimeOffset.Now,
                        UserId = context.Users.Skip(4).First().Id
                    }
                );
                context.SaveChanges();
            }

            // Seed data for ReceiptItem
            if (!context.ReceiptItems.Any())
            {
                context.ReceiptItems.AddRange(
                    new ReceiptItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 1,
                        UnitPrice = 100,
                        TotalPrice = 100,
                        ProductId = context.Products.First().Id,
                        ReceiptId = context.Receipts.First().Id
                    },
                    new ReceiptItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 2,
                        UnitPrice = 200,
                        TotalPrice = 400,
                        ProductId = context.Products.Skip(1).First().Id,
                        ReceiptId = context.Receipts.Skip(1).First().Id
                    },
                    new ReceiptItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 3,
                        UnitPrice = 300,
                        TotalPrice = 900,
                        ProductId = context.Products.Skip(2).First().Id,
                        ReceiptId = context.Receipts.Skip(2).First().Id
                    },
                    new ReceiptItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 4,
                        UnitPrice = 400,
                        TotalPrice = 1600,
                        ProductId = context.Products.Skip(3).First().Id,
                        ReceiptId = context.Receipts.Skip(3).First().Id
                    },
                    new ReceiptItem
                    {
                        Id = Guid.NewGuid(),
                        Quantity = 5,
                        UnitPrice = 500,
                        TotalPrice = 2500,
                        ProductId = context.Products.Skip(4).First().Id,
                        ReceiptId = context.Receipts.Skip(4).First().Id
                    }
                );
                context.SaveChanges();
            }

            // Seed data for Suplier
            if (!context.Supliers.Any())
            {
                context.Supliers.AddRange(
                    new Suplier { Id = Guid.NewGuid(), Name = "Suplier1", ContactInfo = "ContactInfo1" },
                    new Suplier { Id = Guid.NewGuid(), Name = "Suplier2", ContactInfo = "ContactInfo2" },
                    new Suplier { Id = Guid.NewGuid(), Name = "Suplier3", ContactInfo = "ContactInfo3" },
                    new Suplier { Id = Guid.NewGuid(), Name = "Suplier4", ContactInfo = "ContactInfo4" },
                    new Suplier { Id = Guid.NewGuid(), Name = "Suplier5", ContactInfo = "ContactInfo5" }
                );
                context.SaveChanges();
            }
        }
    }
}

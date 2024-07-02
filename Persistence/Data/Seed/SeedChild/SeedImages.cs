using Domain.Entities;
using Persistence;

public class SeedImages
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Images.Any())
        {
            context.Images.AddRange(
                new Image { ImagePath = "image1.jpg" },
                new Image { ImagePath = "image2.jpg" },
                new Image { ImagePath = "image3.jpg" },
                new Image { ImagePath = "image4.jpg" },
                new Image { ImagePath = "image5.jpg" }
            );
            context.SaveChanges();
        }
    }
}

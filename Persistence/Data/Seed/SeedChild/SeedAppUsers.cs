using Domain.Entities;
using Persistence;

public class SeedAppUsers
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new AppUser { UserName = "user1", FirstName = "First1", LastName = "Last1", Email = "user1@example.com" },
                new AppUser { UserName = "user2", FirstName = "First2", LastName = "Last2", Email = "user2@example.com" },
                new AppUser { UserName = "user3", FirstName = "First3", LastName = "Last3", Email = "user3@example.com" },
                new AppUser { UserName = "user4", FirstName = "First4", LastName = "Last4", Email = "user4@example.com" },
                new AppUser { UserName = "user5", FirstName = "First5", LastName = "Last5", Email = "user5@example.com" }
            );
            context.SaveChanges();
        }
    }
}

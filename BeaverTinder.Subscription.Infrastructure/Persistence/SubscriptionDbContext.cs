using BeaverTinder.Subscription.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Subscription.Infrastructure.Persistence;

public sealed class SubscriptionDbContext: DbContext
{
    public DbSet<Core.Entities.Subscription> Subscriptions { get; set; }
    public DbSet<UserSubscription> UserSubscriptions { get; set; }

    public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options)
        : base(options)
    { 
        // Database.Migrate();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Core.Entities.Subscription>().HasData(
            new Core.Entities.Subscription()
            {
                Name = "More likes",
                Description = "Increase your allowed likes count to 40!",
                Id = 1,
                PricePerMonth = 300,
                RoleId = 4,
                RoleName = "UserMoreLikes"
            },
            new Core.Entities.Subscription()
            {
                Name = "More likes and map",
                Description = "Increase your allowed likes count to 50 and get the opportunity to see another beaver on the map!",
                Id = 2,
                PricePerMonth = 500,
                RoleId = 5,
                RoleName = "UserMoreLikesAndMap"
            }
        );
        builder.Entity<UserSubscription>().HasKey(u => new { u.UserId, SubsId = u.SubId});
        base.OnModelCreating(builder);
        
        
    }
}
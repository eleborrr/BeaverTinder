using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public sealed class ApplicationDbContext: IdentityDbContext<User>
{
    public DbSet<Like> Likes { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public new DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<Payment> Payments {get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<UserSubscription> UserSubscriptions { get; set; } = null!;
    public DbSet<UserGeolocation> Geolocations { get; set; } = null!;
    public DbSet<UserToVk> UserToVks { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .Ignore(u => u.PhoneNumber)
            .Ignore(u => u.PhoneNumberConfirmed);
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN",
                LikesCountAllowed = int.MaxValue,
                LocationViewAllowed = true
            },
            new Role
            {
                Id = "2",
                Name = "Moderator",
                NormalizedName = "MODERATOR",
                LikesCountAllowed = int.MaxValue,
                LocationViewAllowed = true
            },
            new Role
            {
                Id = "3",
                Name = "StandartUser",
                NormalizedName = "STANDARTUSER",
                LikesCountAllowed = 20,
                LocationViewAllowed = false
            },
            new Role()
            {
                Id = "4",
                Name = "UserMoreLikes",
                NormalizedName = "USERMORELIKES",
                LikesCountAllowed = 40,
                LocationViewAllowed = false
            },
            new Role()
            {
                Id = "5",
                Name = "UserMoreLikesAndMap",
                NormalizedName = "USERMORELIKESANDMAP",
                LikesCountAllowed = 50,
                LocationViewAllowed = true
            });
        modelBuilder.Entity<Subscription>().HasData(
            new Subscription()
            {
                Name = "More likes",
                Description = "Increase your allowed likes count to 40!",
                Id = 1,
                PricePerMonth = 300,
                RoleId = 4,
                RoleName = "UserMoreLikes"
            },
            new Subscription()
            {
                Name = "More likes and map",
                Description = "Increase your allowed likes count to 50 and get the opportunity to see another beaver on the map!",
                Id = 2,
                PricePerMonth = 500,
                RoleId = 5,
                RoleName = "UserMoreLikesAndMap"
            }
        );
        modelBuilder.Entity<UserSubscription>().HasKey(u => new { u.UserId, u.SubsId});
        modelBuilder.Entity<UserToVk>().HasKey(x => new { Id = x.UserId, x.VkId });
        base.OnModelCreating(modelBuilder);
        
        
    }
}
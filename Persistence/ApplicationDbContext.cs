using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Persistence;

public class ApplicationDbContext: IdentityDbContext<User>
{
    public DbSet<Like> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Payment> Payments {get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<UserSubscription> UserSubscriptions { get; set; }
    public DbSet<UserGeolocation> Geolocations { get; set; }

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
        base.OnModelCreating(modelBuilder);
    }
}
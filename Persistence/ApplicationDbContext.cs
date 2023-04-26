using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDbContext: IdentityDbContext<User>
{
    public DbSet<Like> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Role> Roles { get; set; }
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
        
        base.OnModelCreating(modelBuilder);
    }
}
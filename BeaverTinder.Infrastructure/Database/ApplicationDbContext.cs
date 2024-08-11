using BeaverTinder.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Infrastructure.Database;

public sealed class ApplicationDbContext: IdentityDbContext<User>
{
    public DbSet<Like> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<FileToMessage> Files { get; set; }
    public DbSet<SupportChatMessage> SupportChatMessages { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<SupportRoom> SupportRooms { get; set; }
    
    public new DbSet<Role> Roles { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<UserGeolocation> Geolocations { get; set; }
    public DbSet<UserToVk> UserToVks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { 
        Database.Migrate();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .Ignore(u => u.PhoneNumber)
            .Ignore(u => u.PhoneNumberConfirmed);
        builder.Entity<User>().HasData(
            new User()
            {
                Id = "1",
                UserName = "Admin",
                FirstName = "Gleb",
                LastName = "Bober",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = true,
                Gender = "Male"
            });
        builder.Entity<Role>().HasData(
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
        
        builder.Entity<UserToVk>().HasKey(x => new { Id = x.UserId, x.VkId });
        base.OnModelCreating(builder);
        
        
    }
}
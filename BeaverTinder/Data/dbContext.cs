using BeaverTinder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BeaverTinder.DataBase;

using Microsoft.EntityFrameworkCore;

public class dbContext: IdentityDbContext<User>
{
    public DbSet<Like> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    public dbContext(){}
    
    public dbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .Ignore(u => u.PhoneNumber)
            .Ignore(u => u.PhoneNumberConfirmed);
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            },
            new IdentityRole
            {
                Id = "3",
                Name = "USER",
                NormalizedName = "USER"

            });
        
        base.OnModelCreating(modelBuilder);
    }
}
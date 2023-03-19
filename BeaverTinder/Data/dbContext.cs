using BeaverTinder.Models;

namespace BeaverTinder.DataBase;

using Microsoft.EntityFrameworkCore;

public class dbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    public dbContext(){}
    
    public dbContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // конфигурация модели данных
        // modelBuilder.Entity<User>().HasKey(u => u.Id);
        // modelBuilder.Entity<Like>().HasKey(l => l.Id);
        // modelBuilder.Entity<Message>().HasKey(m => m.Id);
        // modelBuilder.Entity<Role>().HasKey(r => r.Id);
        var adminRole = new Role()
        {
            Id = 1,
            Name = "Admin"
        };
        var moderatorRole = new Role()
        {
            Id = 2,
            Name = "Moderator"
        };
        var userRole = new Role()
        {
            Id = 3,
            Name = "User"
        };
        
        
        var admin = new User()
        {
            Id = 1,
            UserName = "Glebster",
            Email = "gleb-petuhov@mail.ru",
            Confirmed = true,
            PasswordHash = "12345",
            FirstName = "Gleb",
            LastName = "Petukhov",
            About = "cool dude bruv",
            Gender = "male",
            DateOfBirth = DateTime.Parse("21.11.2002"),
            ImageId = 1,
            LastLoginDate = DateTime.Now,
            RoleId = 1
        };
        var loh = new User()
        {
            Id = 2,
            UserName = "rafaellox",
            Email = "@mail.ru",
            Confirmed = true,
            PasswordHash = "rafael.lox.2004",
            FirstName = "Rafael",
            LastName = "Ishmuhametov",
            About = "no comments",
            Gender = "male",
            DateOfBirth = DateTime.Parse("02.01.2004"),
            ImageId = 1,
            LastLoginDate = DateTime.Now,
            RoleId = 3
        };

        modelBuilder.Entity<Role>().HasData(adminRole, userRole, moderatorRole);
        modelBuilder.Entity<User>().HasData(admin, loh);
        base.OnModelCreating(modelBuilder);
    }
}
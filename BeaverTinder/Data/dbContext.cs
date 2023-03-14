using BeaverTinder.Models;

namespace BeaverTinder.DataBase;

using Microsoft.EntityFrameworkCore;

public class dbContext: DbContext
{
    public dbContext(DbContextOptions<dbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // конфигурация модели данных
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Like>().HasKey(l => l.Id);
        modelBuilder.Entity<Message>().HasKey(m => m.Id);
        modelBuilder.Entity<Role>().HasKey(r => r.Id);
        //TODO
        // modelBuilder.Entity<Message>()
        //     .HasOne(p => p.Author)
        //     .WithMany(u => u.Posts)
        //     .HasForeignKey(p => p.AuthorId);
    }
}
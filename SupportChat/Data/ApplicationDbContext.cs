using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SupportChat.Data;

public sealed class ApplicationDbContext: DbContext
{
    public DbSet<SupportChatMessage> SupportChatMessages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { 
    }
}
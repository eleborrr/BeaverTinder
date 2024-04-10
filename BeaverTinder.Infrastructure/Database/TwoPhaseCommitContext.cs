using BeaverTinder.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Infrastructure.Database.Contexts;

public class TwoPhaseCommitContext : DbContext
{
    public TwoPhaseCommitContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Node> Nodes { get; set; }
    public DbSet<NodeState> NodeStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Node>().HasData(
            new Node("Subscription") { Id = Guid.NewGuid() },
            new Node("Payment") { Id = Guid.NewGuid() }
        );
    }
}

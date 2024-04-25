using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Payment.Infrastructure.Persistence;

public sealed class PaymentDbContext: DbContext
{
    public DbSet<Core.Entities.Payment> Payments {get; set; }

    public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
}
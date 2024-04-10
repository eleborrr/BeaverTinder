using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Infrastructure.Database.Contexts;

namespace BeaverTinder.Infrastructure.Database.Repositories;

internal sealed class UnitOfWork: IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext) => _dbContext = dbContext;
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
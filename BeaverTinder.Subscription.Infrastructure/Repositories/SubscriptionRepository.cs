using BeaverTinder.Subscription.Core.Abstractions.Repositories;
using BeaverTinder.Subscription.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Subscription.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly SubscriptionDbContext _dbContext;

    public SubscriptionRepository(SubscriptionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Core.Entities.Subscription>> GetAllASync(CancellationToken cancellationToken)
    {
        return await _dbContext.Subscriptions.ToListAsync(cancellationToken);
    }

    public async Task<Core.Entities.Subscription?>GetBySubscriptionIdAsync(int subsId)
    {
        return await _dbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == subsId);
    }
}
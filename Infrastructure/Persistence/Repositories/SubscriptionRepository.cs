using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public SubscriptionRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<IEnumerable<Subscription>> GetAllASync(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Subscriptions.ToListAsync(cancellationToken);
    }

    public async Task<Subscription?>GetBySubscriptionIdAsync(int subsId)
    {
        return await _applicationDbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == subsId);
    }
}
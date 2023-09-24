using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class UserSubscriptionRepository : IUserSubscriptionRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserSubscriptionRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<IEnumerable<UserSubscription>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.UserSubscriptions.ToListAsync(cancellationToken);
    }

    public async Task<List<UserSubscription>> GetSubscriptionsByUserIdAsync(string userId)
    {
        return await _applicationDbContext.UserSubscriptions.Where(x => x.UserId == userId).ToListAsync();
    }
    
    public async Task<List<UserSubscription>> GetActiveSubscriptionsByUserIdAsync(string userId)
    {
        return await _applicationDbContext.UserSubscriptions.Where(x => x.UserId == userId && x.Active).ToListAsync();
    }

    public async Task<UserSubscription?> GetUserSubscriptionByUserIdAndSubsIdAsync(int subsId, string userId)
    {
        return await _applicationDbContext.UserSubscriptions.FirstOrDefaultAsync(x =>
            x.UserId == userId && x.SubsId == subsId);
    }

    public async Task AddUserSubscriptionAsync(int subsId, string userId)
    {
        var userSub = new UserSubscription()
        {
            Active = true,
            Expires = DateTime.Now + TimeSpan.FromDays(30),
            SubsId = subsId,
            UserId = userId
        };
        await _applicationDbContext.UserSubscriptions.AddAsync(userSub);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task UpdateUserSubAsync(int subsId, string userId)
    {
        var sub = await _applicationDbContext.UserSubscriptions.FirstOrDefaultAsync(x =>
            x.SubsId == subsId && x.UserId == userId);
        
        if (!sub!.Active)
        {
            sub.Active = true;
            sub.Expires = DateTime.Now + TimeSpan.FromDays(30);
            await _applicationDbContext.SaveChangesAsync();
        }
    }

    public async Task SaveAsync()
    {
        await _applicationDbContext.SaveChangesAsync();
    }
}
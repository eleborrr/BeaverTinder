using BeaverTinder.Subscription.Core.Abstractions.Repositories;
using BeaverTinder.Subscription.Core.Entities;
using BeaverTinder.Subscription.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Subscription.Infrastructure.Repositories;

public class UserSubscriptionRepository : IUserSubscriptionRepository
{
    private readonly SubscriptionDbContext _dbContext;

    public UserSubscriptionRepository(SubscriptionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<UserSubscription>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.UserSubscriptions.ToListAsync(cancellationToken);
    }

    public async Task<List<UserSubscription>> GetSubscriptionsByUserIdAsync(string userId)
    {
        return await _dbContext.UserSubscriptions.Where(x => x.UserId == userId).ToListAsync();
    }
    
    public async Task<List<UserSubscription>> GetActiveSubscriptionsByUserIdAsync(string userId)
    {
        return await _dbContext.UserSubscriptions.Where(x => x.UserId == userId && x.Active).ToListAsync();
    }

    public async Task<UserSubscription?> GetUserSubscriptionByUserIdAndSubsIdAsync(int subsId, string userId)
    {
        return await _dbContext.UserSubscriptions.FirstOrDefaultAsync(x =>
            x.UserId == userId && x.SubId == subsId);
    }

    public async Task AddUserSubscriptionAsync(int subsId, string userId)
    {
        var subInfo = _dbContext.Subscriptions.First(s => s.Id == subsId);
        var userSub = new UserSubscription()
        {
            Active = true,
            Expires = DateTime.Now + TimeSpan.FromDays(30),
            SubId = subsId,
            UserId = userId,
            Description = subInfo.Description,
            RoleName = subInfo.RoleName,
            PricePerMonth = subInfo.PricePerMonth,
            RoleId = subInfo.RoleId
        };
        await _dbContext.UserSubscriptions.AddAsync(userSub);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserSubAsync(int subsId, string userId)
    {
        var sub = await _dbContext.UserSubscriptions.FirstOrDefaultAsync(x =>
            x.SubId == subsId && x.UserId == userId);
        
        if (!sub!.Active)
        {
            sub.Active = true;
            sub.Expires = DateTime.Now + TimeSpan.FromDays(30);
            await _dbContext.SaveChangesAsync();
        }
    }
    
    public async Task DeleteUserSub(int subId, string userId)
    {
        var sub = await _dbContext.UserSubscriptions.FirstOrDefaultAsync(x =>
            x.SubId == subId && x.UserId == userId);
        if (sub is null)
            return;
        _dbContext.UserSubscriptions.Remove(sub);
    }


    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
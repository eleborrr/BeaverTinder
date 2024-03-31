using BeaverTinder.Subscription.Core.Entities;

namespace BeaverTinder.Subscription.Core.Abstractions.Repositories;

public interface IUserSubscriptionRepository
{
    public Task<IEnumerable<UserSubscription>> GetAllAsync(CancellationToken cancellationToken);
    public Task<List<UserSubscription>> GetSubscriptionsByUserIdAsync(string userId);
    public Task<List<UserSubscription>> GetActiveSubscriptionsByUserIdAsync(string userId);
    public Task<UserSubscription?> GetUserSubscriptionByUserIdAndSubsIdAsync(int subsId, string userId);
    public Task AddUserSubscriptionAsync(int subsId, string userId);
    public Task UpdateUserSubAsync(int subsId, string userId);
    public Task SaveAsync();
}
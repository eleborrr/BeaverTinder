using BeaverTinder.Shared.Dto.Subscription;

namespace BeaverTinder.Subscription.Core.Abstractions.Services.Subscriptions;

public interface ISubscriptionService
{
    public Task AddSubscriptionToUser(int subsId, string userId);
    public Task<IEnumerable<Entities.Subscription>> GetAllAsync();
    public Task<SubscriptionInfoDto> GetUserActiveSubscription(string userId);
}
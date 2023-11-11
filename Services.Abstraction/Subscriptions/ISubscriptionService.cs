using Contracts.Dto.Subscription;
using Domain.Entities;

namespace Services.Abstraction.Subscriptions;

public interface ISubscriptionService
{
    public Task AddSubscriptionToUser(int subsId, string userId);
    public Task<IEnumerable<Subscription>> GetAllAsync();
    public Task<SubscriptionInfoDto> GetUserActiveSubscription(string userId);
}
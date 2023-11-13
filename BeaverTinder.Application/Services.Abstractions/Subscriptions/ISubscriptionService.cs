using BeaverTinder.Application.Dto.Subscription;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Services.Abstractions.Subscriptions;

public interface ISubscriptionService
{
    public Task AddSubscriptionToUser(int subsId, string userId);
    public Task<IEnumerable<Subscription>> GetAllAsync();
    public Task<SubscriptionInfoDto> GetUserActiveSubscription(string userId);
}
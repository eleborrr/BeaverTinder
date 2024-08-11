namespace BeaverTinder.Subscription.Core.Abstractions.Repositories;

public interface ISubscriptionRepository
{
    public Task<IEnumerable<Entities.Subscription>> GetAllASync(CancellationToken cancellationToken);
    public Task<Entities.Subscription?> GetBySubscriptionIdAsync(int subsId);
}
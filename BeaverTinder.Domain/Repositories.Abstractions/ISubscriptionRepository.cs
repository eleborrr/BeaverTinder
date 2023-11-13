using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface ISubscriptionRepository
{
    public Task<IEnumerable<Subscription>> GetAllASync(CancellationToken cancellationToken);
    public Task<Subscription?> GetBySubscriptionIdAsync(int subsId);
}
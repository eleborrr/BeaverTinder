using Domain.Entities;

namespace Domain.Repositories;

public interface ISubscriptionRepository
{
    public Task<IEnumerable<Subscription>> GetAllASync(CancellationToken cancellationToken);
    public Task<Subscription?> GetBySubscriptionIdAsync(int subsId);
}
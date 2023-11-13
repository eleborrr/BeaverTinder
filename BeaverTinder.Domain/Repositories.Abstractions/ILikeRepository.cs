using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface ILikeRepository
{
    public Task<IEnumerable<Like>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(Like like);
}
using Domain.Entities;

namespace Domain.Repositories;

public interface ILikeRepository
{
    public Task<IEnumerable<Like>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(Like like);
}
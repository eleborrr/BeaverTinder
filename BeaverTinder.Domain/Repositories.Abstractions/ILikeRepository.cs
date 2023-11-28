using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface ILikeRepository
{
    public Task<IEnumerable<Like>> GetAll();
    public Task AddAsync(Like like);
}
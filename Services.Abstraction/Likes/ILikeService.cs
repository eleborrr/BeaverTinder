using Domain.Entities;

namespace Services.Abstraction.Likes;

public interface ILikeService
{
    public Task<IEnumerable<Like>> GetAllAsync();
}
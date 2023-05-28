using Domain.Entities;

namespace Services.Abstraction.Likes;

public interface ILikeService
{
    public Task<IEnumerable<Like>> GetAllAsync();
    public Task<bool> IsMutualSympathy(User user1, User user2);
}
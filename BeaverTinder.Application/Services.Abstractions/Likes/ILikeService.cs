using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Services.Abstractions.Likes;

public interface ILikeService
{
    public Task<IEnumerable<Like>> GetAllAsync();
    public Task<bool> IsMutualSympathy(User user1, User user2);
}
using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Likes;

namespace Services.Likes;

public class LikeService: ILikeService
{
    private readonly IRepositoryManager _repositoryManager;

    public LikeService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<bool> IsMutualSympathy(User user1, User user2)
    {
        return (await _repositoryManager.LikeRepository.GetAllAsync(default))
               .Any(l => l.UserId == user1.Id && l.LikedUserId == user2.Id && l.Sympathy)
               &&
               (await _repositoryManager.LikeRepository.GetAllAsync(default))
               .Any(l => l.UserId == user2.Id && l.LikedUserId == user1.Id && l.Sympathy);
    }
    
    public async Task<IEnumerable<Like>> GetAllAsync()
    {
        return await _repositoryManager.LikeRepository.GetAllAsync(default);
    }
}
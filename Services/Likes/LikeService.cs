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

    public async Task<IEnumerable<Like>> GetAllAsync()
    {
        return await _repositoryManager.LikeRepository.GetAllAsync(default);
    }
}
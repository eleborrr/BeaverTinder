using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Services.Abstraction;
using Services.Abstraction.FindBeaver;

namespace Services.FindBeaver;

public class FindBeaverService: IFindBeaverService
{
    private UserManager<User> _userManager;
    private IRepositoryManager _repositoryManager;
    private IMemoryCache _memoryCache;

    public FindBeaverService(UserManager<User> userManager, IRepositoryManager repositoryManager, IMemoryCache memoryCache)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
        _memoryCache = memoryCache;
    }

    public async Task<User?> GetNextBeaver(User currentUser)
    {
        // логика, где поиск идет так же по местоположению.
        // TODO эффективный алгоритм выдачи пользователя!
        
        _memoryCache.TryGetValue(currentUser.Id, out List<User>? likesCache);
        if (likesCache == null)
        {
            var likes = await _repositoryManager.LikeRepository.GetAllAsync(default); // ???

            var filtredBeavers = _userManager.Users.AsEnumerable()
                .Where(u => likes.Count(l => l.UserId == currentUser.Id && l.LikedUserId == u.Id ) == 0 && u.Id != currentUser.Id) // проверяем чтобы не попадались лайкнутые
                .OrderBy(u => Math.Abs(currentUser.DateOfBirth.Year - u.DateOfBirth.Year))
                .Take(10)
                .ToList();
                _memoryCache.Set(currentUser.Id, filtredBeavers,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            return filtredBeavers.FirstOrDefault();
        }

        var returnUser = likesCache.FirstOrDefault();
        
        return returnUser;
    }

    public Task Like(string userId1, string userId2)
    {
        if (_memoryCache.TryGetValue(userId1, out List<User>? likesCache))
        {
            _memoryCache.Set(userId1, likesCache.Skip(1),
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
        }

        var newLike = new Like() { UserId = userId1, LikedUserId = userId2, LikeDate = DateTime.Now };
        return _repositoryManager.LikeRepository.AddAsync(newLike);
    }

    public Task Dislike(string user1, string user2)
    {
        throw new NotImplementedException();
    }
}
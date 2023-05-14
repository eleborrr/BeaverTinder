using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Services.Abstraction;
using Services.Abstraction.FindBeaver;
using Services.Abstraction.Likes;

namespace Services.FindBeaver;

public class FindBeaverService: IFindBeaverService
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMemoryCache _memoryCache;
    private readonly RoleManager<Role> _roleManager;
    private readonly ILikeService _likeService;

    public FindBeaverService(UserManager<User> userManager, IRepositoryManager repositoryManager, IMemoryCache memoryCache, RoleManager<Role> roleManager, ILikeService likeService)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
        _memoryCache = memoryCache;
        _roleManager = roleManager;
        _likeService = likeService;
    }

    public async Task<User?> GetNextBeaver(User currentUser, Role userRole)
    {
        // логика, где поиск идет так же по местоположению.
        // TODO учет расстояния, ограничение по подписке.

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

    //TODO юзер будет приходить через жвт??
    public async Task AddSympathy(User user1, string userId2, bool sympathy, Role userRole)
    {
        if (!await CheckSubscriptionLikePermission(user1, userRole))
            throw new Exception("Like limit!");
        MemoryCacheUpdate(user1.Id);

        var newLike = new Like() { UserId = user1.Id, LikedUserId = userId2, LikeDate = DateTime.Now, Sympathy = sympathy};
        await _repositoryManager.LikeRepository.AddAsync(newLike);
    }
    
    private void MemoryCacheUpdate(string userId)
    {
        if (_memoryCache.TryGetValue(userId, out List<User>? likesCache))
        {
            _memoryCache.Set(userId, likesCache.Skip(1),
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
        }
    }
    private async Task<bool> CheckSubscriptionLikePermission(User? user, Role role)
    {
        if (user is null)
            return false;
        //TODO make checks
        return (await _likeService.GetAllAsync())
            .Count(l => l.LikeDate.Date.Day == DateTime.Today.Day) <= role.LikesCountAllowed;
        // TODO return custom Exception
    }
}
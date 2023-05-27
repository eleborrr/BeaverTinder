using Contracts.Responses.Search;
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
    private readonly IServiceManager _serviceManager;

    public FindBeaverService(UserManager<User> userManager, IRepositoryManager repositoryManager, IMemoryCache memoryCache, RoleManager<Role> roleManager, IServiceManager serviceManager)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
        _memoryCache = memoryCache;
        _serviceManager = serviceManager;
    }

    public async Task<SearchUserResultDto> GetNextBeaver(User? currentUser, Role? userRole)
    {
        // логика, где поиск идет так же по местоположению.
        // TODO учет расстояния, ограничение по подписке.
        if (currentUser is null)
            return new SearchUserResultDto
            {
                Successful = false,
                Message = "Logged in user error",
                StatusCode = 400
            };
        
        if (userRole is null)
            return new SearchUserResultDto
            {
                Successful = false,
                Message = "Can't get user's role",
                StatusCode = 500
            };

        _memoryCache.TryGetValue(currentUser.Id, out List<User>? likesCache);
        if (likesCache == null)
        {
            var likes = await _repositoryManager.LikeRepository.GetAllAsync(default); // ???

            var filtredBeavers = _userManager.Users.AsQueryable()
                .Where(u => likes.Count(l => l.UserId == currentUser.Id && l.LikedUserId == u.Id ) == 0 && u.Id != currentUser.Id) // проверяем чтобы не попадались лайкнутые
                .OrderBy(u => Math.Abs(currentUser.DateOfBirth.Year - u.DateOfBirth.Year))
                .Take(10)
                .ToList();
                _memoryCache.Set(currentUser.Id, filtredBeavers,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            //TODO possible error?
            var returnUserCache = filtredBeavers.FirstOrDefault();
            
            if (returnUserCache is null)
                return new SearchUserResultDto()
                {
                    Successful = false,
                    Message = "Beaver queue error",
                    StatusCode = 500
                };

            var curUserGeoloc = await _serviceManager.GeolocationService.GetByUserId(currentUser.Id);
            var likedUserGeoloc = await _serviceManager.GeolocationService.GetByUserId(returnUserCache.Id);

            var distanceInKm = Convert.ToInt32(
                System.Math.Ceiling(
                    await _serviceManager.GeolocationService.GetDistance(curUserGeoloc, likedUserGeoloc)));

            return new SearchUserResultDto
            {
                Id = returnUserCache.Id,
                About = returnUserCache.About,
                FirstName = returnUserCache.FirstName,
                LastName = returnUserCache.LastName,
                Gender = returnUserCache.Gender,
                Age = DateTime.Now.Year - returnUserCache.DateOfBirth.Year,
                DistanceInKm = distanceInKm,
                Message = "ok",
                StatusCode = 200,
                Successful = true
            }; 
        }

        var returnUser = likesCache.FirstOrDefault();

        if (returnUser is null)
            return new SearchUserResultDto()
            {
                Successful = false,
                Message = "Beaver queue error",
                StatusCode = 500
            };
        
        
        //TODO possible error?
        return new SearchUserResultDto
        {
            Id = returnUser.Id,
            About = returnUser.About,
            FirstName = returnUser.FirstName,
            LastName = returnUser.LastName,
            Gender = returnUser.Gender,
            Age = returnUser.DateOfBirth.Year - DateTime.Now.Year,
            Message = "ok",
            StatusCode = 200,
            Successful = true
        };
    }

    
    //TODO memory cache
    public async Task<SearchUserResultDto> GetNextSympathy(User? currentUser)
    {
        if (currentUser is null)
            return new SearchUserResultDto
            {
                Successful = false,
                Message = "Logged in user error",
                StatusCode = 400
            };
        var likes = await _repositoryManager.LikeRepository.GetAllAsync(default); // ???

        var filtredBeavers = _userManager.Users.AsQueryable()
            .Where(u => likes.Count(l => l.UserId ==  u.Id && l.LikedUserId ==currentUser.Id ) != 0 
                        && likes.Count(l => l.UserId == currentUser.Id && l.LikedUserId ==  u.Id) == 0
                        && u.Id != currentUser.Id) // проверяем чтобы попадались лайкнутые
            .OrderBy(u => Math.Abs(currentUser.DateOfBirth.Year - u.DateOfBirth.Year))
            .Take(10)
            .ToList();
        
        var returnUserCache = filtredBeavers.FirstOrDefault();
        return new SearchUserResultDto
        {
            Id = returnUserCache.Id,
            About = returnUserCache.About,
            FirstName = returnUserCache.FirstName,
            LastName = returnUserCache.LastName,
            Gender = returnUserCache.Gender,
            Age = DateTime.Now.Year - returnUserCache.DateOfBirth.Year,
            Message = "ok",
            StatusCode = 200,
            Successful = true
        }; 
    }

    //TODO юзер будет приходить через жвт??
    public async Task<LikeResponseDto> AddSympathy(User? user1, string userId2, bool sympathy, Role? userRole)
    {
        if (user1 is null)
            return new LikeResponseDto(LikeResponseStatus.Fail, "Your account not found");

        var user2 = await _userManager.FindByIdAsync(userId2);
        if (user2 is null)
            return new LikeResponseDto(LikeResponseStatus.Fail, "Can't find user with that id");

        if (userRole is null)
            return new LikeResponseDto(LikeResponseStatus.Fail, "Can't get user's role");

        if (!await CheckSubscriptionLikePermission(user1, userRole))
            return new LikeResponseDto(LikeResponseStatus.Fail, "Like limit!");
        MemoryCacheUpdate(user1.Id);

        var newLike = new Like() { UserId = user1.Id, LikedUserId = userId2, LikeDate = DateTime.Now, Sympathy = sympathy};
        await _repositoryManager.LikeRepository.AddAsync(newLike);
        return new LikeResponseDto(LikeResponseStatus.Ok);
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
        return (await _serviceManager.LikeService.GetAllAsync())
            .Count(l => l.LikeDate.Date.Day == DateTime.Today.Day && l.UserId == user.Id) <= role.LikesCountAllowed;
        // TODO return custom Exception
    }
}
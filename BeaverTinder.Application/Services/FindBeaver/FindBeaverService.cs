using System.Globalization;
using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Services.Abstractions.FindBeaver;
using BeaverTinder.Application.Services.Abstractions.Geolocation;
using BeaverTinder.Application.Services.Abstractions.Likes;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace BeaverTinder.Application.Services.FindBeaver;

public class FindBeaverService: IFindBeaverService
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMemoryCache _memoryCache;
    private readonly ILikeService _likeService;
    private readonly IGeolocationService _geolocationService;

    public FindBeaverService(UserManager<User> userManager, IRepositoryManager repositoryManager, IMemoryCache memoryCache, ILikeService likeService, IGeolocationService geolocationService)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
        _memoryCache = memoryCache;
        _likeService = likeService;
        _geolocationService = geolocationService;
    }

    public async Task<SearchUserResultDto> GetNextBeaver(User? currentUser, Role? userRole)
    {
        // логика, где поиск идет так же по местоположению.
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

            var filteredBeavers = _userManager.Users.AsEnumerable()
                .Where(u => !likes.Any(l => l.UserId == currentUser.Id && l.LikedUserId == u.Id ) && u.Id != currentUser.Id) // проверяем чтобы не попадались лайкнутые
                .OrderBy(u => Math.Abs(_geolocationService.GetDistance(currentUser, u).Result))
                .ThenBy(u => currentUser.DateOfBirth.Year - u.DateOfBirth.Year)
                .Take(10)
                .ToList();
                _memoryCache.Set(currentUser.Id, filteredBeavers,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            var returnUserCache = filteredBeavers.FirstOrDefault();
            
            if (returnUserCache is null)
                return new SearchUserResultDto()
                {
                    Successful = false,
                    Message = "Beaver queue error",
                    StatusCode = 500
                };

            var curUserGeolocation = await _geolocationService.GetByUserId(currentUser.Id);
            var likedUserGeolocation = await _geolocationService.GetByUserId(returnUserCache.Id);

            if (curUserGeolocation is null || likedUserGeolocation is null)
                return new SearchUserResultDto()
                {
                    Successful = false,
                    Message = "Geolocation binding error",
                    StatusCode = 500
                };

            var distanceInKm = 
                Math.Ceiling(
                    await _geolocationService.GetDistance(curUserGeolocation, likedUserGeolocation)).ToString(CultureInfo.CurrentCulture);

            return new SearchUserResultDto
            {
                Id = returnUserCache.Id,
                About = returnUserCache.About ?? "",
                FirstName = returnUserCache.FirstName,
                LastName = returnUserCache.LastName,
                Gender = returnUserCache.Gender,
                Age = DateTime.Now.Year - returnUserCache.DateOfBirth.Year,
                DistanceInKm = distanceInKm,
                Message = "ok",
                StatusCode = 200,
                Successful = true,
                Image = returnUserCache.Image!,
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

        return new SearchUserResultDto
        {
            Id = returnUser.Id,
            About = returnUser.About ?? "",
            FirstName = returnUser.FirstName,
            LastName = returnUser.LastName,
            Gender = returnUser.Gender,
            Age = DateTime.Now.Year - returnUser.DateOfBirth.Year ,
            Message = "ok",
            StatusCode = 200,
            Successful = true,
            Image = returnUser.Image!
        };
    }

    public async Task<SearchUserResultDto> GetNextSympathy(User? currentUser)
    {
        if (currentUser is null)
            return new SearchUserResultDto
            {
                Successful = false,
                Message = "Logged in user error",
                StatusCode = 400
            };
        var likes = (await _repositoryManager.LikeRepository.GetAllAsync(default)).ToList();
        

        var filteredBeavers = _userManager.Users.AsEnumerable()
        .Where(u => likes.Any(l => l.UserId ==  u.Id && l.LikedUserId ==currentUser.Id )
                    && !likes.Any(l => l.UserId == currentUser.Id && l.LikedUserId ==  u.Id)
                    && u.Id != currentUser.Id) // проверяем чтобы попадались лайкнутые
        .OrderBy(u => Math.Abs(currentUser.DateOfBirth.Year - u.DateOfBirth.Year))
        .Take(10)
        .ToList();
        
        var returnUserCache = filteredBeavers.FirstOrDefault();
        if (returnUserCache is null)
            return new SearchUserResultDto()
            {
                Successful = false,
                Message = "Beaver queue error",
                StatusCode = 500
            };
        return new SearchUserResultDto
        {
            Id = returnUserCache.Id,
            About = returnUserCache.About ?? "",
            FirstName = returnUserCache.FirstName,
            LastName = returnUserCache.LastName,
            Gender = returnUserCache.Gender,
            Age = DateTime.Now.Year - returnUserCache.DateOfBirth.Year,
            Message = "ok",
            StatusCode = 200,
            Successful = true,
            Image = returnUserCache.Image!
        }; 
    }
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

        var newLike = new Like { UserId = user1.Id, LikedUserId = userId2, LikeDate = DateTime.Now, Sympathy = sympathy};
        await _repositoryManager.LikeRepository.AddAsync(newLike);
        return new LikeResponseDto(LikeResponseStatus.Ok);
    }
    
    private void MemoryCacheUpdate(string userId)
    {
        if (_memoryCache.TryGetValue(userId, out List<User>? likesCache))
        {
            _memoryCache.Set(userId, likesCache is null? new List<User>() :likesCache.Skip(1),
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
        }
    }
    private async Task<bool> CheckSubscriptionLikePermission(User? user, Role role)
    {
        if (user is null)
            return false;
        return (await _likeService.GetAllAsync())
            .Count(l => l.LikeDate.Date.Day == DateTime.Today.Day && l.UserId == user.Id) <= role.LikesCountAllowed;
    }
}
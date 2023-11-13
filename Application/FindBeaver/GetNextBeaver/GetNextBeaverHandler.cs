using System.Globalization;
using Contracts.Dto.BeaverMatchSearch;
using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Services.Abstraction.Cqrs.Queries;
using Services.Abstraction.Geolocation;

namespace Application.FindBeaver.GetNextBeaver;

public class GetNextBeaverHandler : IQueryHandler<GetNextBeaverQuery, SearchUserResultDto>
{
    
    private readonly IMemoryCache _memoryCache;
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;
    private readonly IGeolocationService _geolocationService;

    public GetNextBeaverHandler(IMemoryCache memoryCache, IRepositoryManager repositoryManager, UserManager<User> userManager, IMediator mediator, IGeolocationService geolocationService)
    {
        _memoryCache = memoryCache;
        _repositoryManager = repositoryManager;
        _userManager = userManager;
        _geolocationService = geolocationService;
    }

    public async Task<Result<SearchUserResultDto>> Handle(
        GetNextBeaverQuery request,
        CancellationToken cancellationToken)
    {
        _memoryCache.TryGetValue(request.CurrentUser!.Id, out List<User>? likesCache);
        if (likesCache == null)
        {
            var likes = await _repositoryManager.LikeRepository.GetAllAsync(default); // ???

            var filteredBeavers = _userManager.Users.AsEnumerable()
                .Where(u => !likes.Any(l => l.UserId == request.CurrentUser.Id && l.LikedUserId == u.Id ) && u.Id != request.CurrentUser.Id) // проверяем чтобы не попадались лайкнутые
                .OrderBy(u => Math.Abs(_geolocationService.GetDistance(request.CurrentUser, u).Result))
                .ThenBy(u => request.CurrentUser.DateOfBirth.Year - u.DateOfBirth.Year)
                .Take(10)
                .ToList();
                _memoryCache.Set(request.CurrentUser.Id, filteredBeavers,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            var returnUserCache = filteredBeavers.FirstOrDefault();
            
            if (returnUserCache is null)
                return new Result<SearchUserResultDto>(new SearchUserResultDto
                {
                    Successful = false,
                    Message = "Beaver queue error",
                    StatusCode = 500
                }, true);

            var curUserGeolocation = await _geolocationService.GetByUserId(request.CurrentUser.Id);
            var likedUserGeolocation = await _geolocationService.GetByUserId(returnUserCache.Id);

            if (curUserGeolocation is null || likedUserGeolocation is null)
                return new Result<SearchUserResultDto>(new SearchUserResultDto
                {
                    Successful = false,
                    Message = "Geolocation binding error",
                    StatusCode = 500
                }, true);

            var distanceInKm = 
                Math.Ceiling(
                    await _geolocationService.GetDistance(curUserGeolocation, likedUserGeolocation)).ToString(CultureInfo.CurrentCulture);

            return new Result<SearchUserResultDto>(new SearchUserResultDto
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
            }, true); 
        }

        var returnUser = likesCache.FirstOrDefault();

        if (returnUser is null)
            return new Result<SearchUserResultDto>(new SearchUserResultDto
            {
                Successful = false,
                Message = "Beaver queue error",
                StatusCode = 500
            }, false);

        return new Result<SearchUserResultDto>(new SearchUserResultDto
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
        }, true);
    }
}
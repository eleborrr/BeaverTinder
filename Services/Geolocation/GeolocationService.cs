using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Geolocation;

namespace Services.Geolocation;

public class GeolocationService: IGeolocationService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;

    public GeolocationService(IRepositoryManager repositoryManager, UserManager<User> userManager)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }

    public async Task AddAsync(string userId, double Latutide, double Longtitude)
    {
        //TODO валидность проверять
        var geolocation = new UserGeolocation()
        {
            Id = userId,
            Longtitude = Longtitude,
            Latutide = Latutide
        };
        await _repositoryManager.GeolocationRepository.AddAsync(geolocation);
    }

    public async Task<UserGeolocation> GetByUserId(string userId)
    {
        return await _repositoryManager.GeolocationRepository.GetByUserIdAsync(userId);
    }
    
    public async Task<IEnumerable<UserGeolocation>> GetAllAsync()
    {
        return await _repositoryManager.GeolocationRepository.GetAllAsync(default);
    }

    public async Task<double> GetDistance(UserGeolocation geolocation1, UserGeolocation geolocation2)
    {
        const int R = 6371;
        return 2 * R * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(geolocation2.Latutide - geolocation1.Latutide), 2)/2) +
                                           Math.Cos(geolocation1.Latutide) * Math.Cos(geolocation2.Latutide) *
                                           Math.Pow(Math.Sin(geolocation2.Longtitude - geolocation1.Longtitude), 2) / 2)
            ;
    }

    public async Task<double> GetDistance(User user1, User user2)
    {
        var geoloc1 = await GetByUserId(user1.Id);
        var geoloc2 = await GetByUserId(user2.Id);
        return await GetDistance(geoloc1, geoloc2);
    }
}
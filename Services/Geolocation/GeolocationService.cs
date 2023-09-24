using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Geolocation;

namespace Services.Geolocation;

public class GeolocationService: IGeolocationService
{
    private readonly IRepositoryManager _repositoryManager;

    public GeolocationService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task AddAsync(string userId, double latitude, double longitude)
    {
        //TODO валидность проверять
        var geolocation = new UserGeolocation()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            Longtitude = longitude,
            Latitude = latitude
        };
        await _repositoryManager.GeolocationRepository.AddAsync(geolocation);
    }

    public async Task<UserGeolocation?> GetByUserId(string userId)
    {
        return await _repositoryManager.GeolocationRepository.GetByUserIdAsync(userId);
    }
    
    public async Task<IEnumerable<UserGeolocation>> GetAllAsync()
    {
        return await _repositoryManager.GeolocationRepository.GetAllAsync(default);
    }

    public Task<double> GetDistance(UserGeolocation geolocation1, UserGeolocation geolocation2)
    {
        const int r = 6371;
        return Task.FromResult(2 * r * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(geolocation2.Latitude - geolocation1.Latitude), 2)/2) +
                                           Math.Cos(geolocation1.Latitude) * Math.Cos(geolocation2.Latitude) *
                                           Math.Pow(Math.Sin(geolocation2.Longtitude - geolocation1.Longtitude), 2) / 2));
    }

    public async Task<double> GetDistance(User user1, User user2)
    {
        var firstUserGeolocation = await GetByUserId(user1.Id);
        var secondUserGeolocation = await GetByUserId(user2.Id);
        return await GetDistance(firstUserGeolocation!, secondUserGeolocation!);
    }

    public async Task Update(string userId, double latitude, double longitude)
    {
        var geoloc = await GetByUserId(userId);
        if (geoloc is not null)
        {
            geoloc.Longtitude = longitude;
            geoloc.Latitude = latitude;
            await _repositoryManager.GeolocationRepository.UpdateAsync(geoloc);
        }
        else
        {
            geoloc = new UserGeolocation
            {
                Id = Guid.NewGuid().ToString(),
                Longtitude = longitude,
                Latitude = latitude,
                UserId = userId
            };
            await _repositoryManager.GeolocationRepository.AddAsync(geoloc);
        }
    }
}
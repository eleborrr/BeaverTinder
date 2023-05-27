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
}
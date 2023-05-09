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
}
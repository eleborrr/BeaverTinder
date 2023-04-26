using Domain.Entities;

namespace Services.Abstraction.Geolocation;

public interface IGeolocationService
{
    public Task AddAsync(int userId, double Latutide, double Longtitude);

    public Task<UserGeolocation> GetByUserId(int userId);

    public Task<IEnumerable<UserGeolocation>> GetAllAsync();
    // отображение геолокации по координатам

    // установка геолокации (?)

    // 
}
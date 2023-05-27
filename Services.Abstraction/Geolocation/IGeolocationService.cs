using Domain.Entities;

namespace Services.Abstraction.Geolocation;

public interface IGeolocationService
{
    public Task AddAsync(string userId, double Latutide, double Longtitude);

    public Task<UserGeolocation> GetByUserId(string userId);

    public Task<IEnumerable<UserGeolocation>> GetAllAsync();
    public Task<double> GetDistance(UserGeolocation geolocation1, UserGeolocation geolocation2);
}
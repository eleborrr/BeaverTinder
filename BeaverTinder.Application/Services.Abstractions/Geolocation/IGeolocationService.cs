using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Services.Abstractions.Geolocation;

public interface IGeolocationService
{
    public Task AddAsync(string userId, double latitude, double longitude);

    public Task<UserGeolocation?> GetByUserId(string userId);
    
    public Task Update(string userId, double latitude, double longitude);

    public Task<IEnumerable<UserGeolocation>> GetAllAsync();
    public Task<double> GetDistance(UserGeolocation geolocation1, UserGeolocation geolocation2);
    public Task<double> GetDistance(User user1, User user2);
}
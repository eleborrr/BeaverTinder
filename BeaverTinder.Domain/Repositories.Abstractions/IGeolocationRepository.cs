using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface IGeolocationRepository
{
    public Task<IEnumerable<UserGeolocation>> GetAllAsync(CancellationToken cancellationToken);
    public Task<string> AddAsync(UserGeolocation geolocation);
    public Task<UserGeolocation?> GetByUserIdAsync(string userId);
    public Task UpdateAsync(UserGeolocation geolocation);
}
using Domain.Entities;

namespace Domain.Repositories;

public interface IGeolocationRepository
{
    public Task<IEnumerable<UserGeolocation>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(UserGeolocation geolocation);
    public Task<UserGeolocation> GetByUserIdAsync(int userId);
}
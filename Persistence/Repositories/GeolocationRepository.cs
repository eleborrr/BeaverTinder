using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class GeolocationRepository: IGeolocationRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public GeolocationRepository(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;
    
    public async Task<IEnumerable<UserGeolocation>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _applicationDbContext.Geolocations.ToListAsync(cancellationToken);
    
    public async Task<UserGeolocation>? GetByUserIdAsync(string userId) =>
        await _applicationDbContext.Geolocations.FirstOrDefaultAsync(x => x.Id == userId);

    public async Task AddAsync(UserGeolocation geolocation)
    {
        await _applicationDbContext.Geolocations.AddAsync(geolocation);
        await _applicationDbContext.SaveChangesAsync();
    }

    public void Remove(Like like) => _applicationDbContext.Likes.Remove(like);
    
}
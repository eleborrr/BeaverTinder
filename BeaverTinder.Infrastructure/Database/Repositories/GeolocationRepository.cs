using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Infrastructure.Database.Repositories;

internal sealed class GeolocationRepository: IGeolocationRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public GeolocationRepository(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;
    
    public async Task<IEnumerable<UserGeolocation>> GetAllAsync(CancellationToken cancellationToken) =>
        await _applicationDbContext.Geolocations.ToListAsync(cancellationToken);
    
    public async Task<UserGeolocation?> GetByUserIdAsync(string userId) =>
        await _applicationDbContext.Geolocations.FirstOrDefaultAsync(x => x.UserId == userId);

    public async Task<string> AddAsync(UserGeolocation geolocation)
    {
        await _applicationDbContext.Geolocations.AddAsync(geolocation);
        await _applicationDbContext.SaveChangesAsync();
        return geolocation.Id;
    }

    public async Task UpdateAsync(UserGeolocation geolocation)
    {
        var geolocationInDb = _applicationDbContext.Geolocations.FirstOrDefault(g => g.Id == geolocation.Id);
        if (geolocationInDb is null)
            return;
        _applicationDbContext.Entry(geolocationInDb).CurrentValues.SetValues(geolocation);
        await _applicationDbContext.SaveChangesAsync();
    }
}
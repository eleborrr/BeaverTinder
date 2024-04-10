using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Infrastructure.Database.Repositories;

public class SupportRoomRepository : ISupportRoomRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SupportRoomRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public IQueryable<SupportRoom> GetAll()
    {
        return _dbContext.SupportRooms.AsQueryable();
    }

    public async Task<SupportRoom?> GetByIdAsync(string roomId)
    {
        return await _dbContext.SupportRooms.Where(r => r.Id == roomId).FirstOrDefaultAsync();
    }

    public async Task<SupportRoom?> GetByUserIdAsync(string userId)
    {
        return await _dbContext.SupportRooms.Where(r => r.FirstUserId == userId).FirstOrDefaultAsync();
    }

    public async Task AddAsync(SupportRoom room)
    {
        await _dbContext.SupportRooms.AddAsync(room);
        await _dbContext.SaveChangesAsync();
    }
}
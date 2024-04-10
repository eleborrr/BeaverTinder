using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Infrastructure.Database.Repositories;

public class RoomRepository: IRoomRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoomRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Room>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Rooms.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Room room)
    {
        await _dbContext.Rooms.AddAsync(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Room?> GetByRoomIdAsync(string roomId)
    {
        return await _dbContext.Rooms.FirstOrDefaultAsync(x => x.Id == roomId);
    }
}
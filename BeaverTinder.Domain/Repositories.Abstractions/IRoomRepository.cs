using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface IRoomRepository
{
    public Task<IEnumerable<Room>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(Room room);
    public Task<Room?> GetByRoomIdAsync(string roomId);
}
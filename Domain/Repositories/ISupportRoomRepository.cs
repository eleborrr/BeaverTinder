using Domain.Entities;

namespace Domain.Repositories;

public interface ISupportRoomRepository
{
    public IQueryable<SupportRoom> GetAll();
    public Task<SupportRoom?> GetByIdAsync(string roomId);
    public Task<SupportRoom?> GetByUserIdAsync(string userId);
    public Task AddAsync(SupportRoom room);
}
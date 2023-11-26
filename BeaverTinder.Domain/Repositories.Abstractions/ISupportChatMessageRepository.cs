using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface ISupportChatMessageRepository
{
    public Task AddAsync(SupportChatMessage message);
    public IQueryable<SupportChatMessage> GetAll();
}
using Domain.Entities;

namespace Domain.Repositories;

public interface ISupportChatMessageRepository
{
    public Task AddAsync(SupportChatMessage message);
    public IQueryable<SupportChatMessage> GetAll();
}
using Domain.Entities;

namespace Domain.Repositories;

public interface IMessageRepository
{
    public Task<IEnumerable<Message>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(Message message);
    public Task<Message?> GetByMessageIdAsync(string messageId);
}
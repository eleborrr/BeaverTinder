using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Domain.Repositories.Abstractions;

public interface IMessageRepository
{
    public Task<IEnumerable<Message>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(Message message);
    public Task<Message?> GetByMessageIdAsync(string messageId);
}
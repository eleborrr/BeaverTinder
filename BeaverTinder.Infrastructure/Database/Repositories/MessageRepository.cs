using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Infrastructure.Database.Repositories;

public class MessageRepository: IMessageRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
        
    public MessageRepository(ApplicationDbContext dbContext)
    {
        _applicationDbContext = dbContext;
    }
    
    public async Task<IEnumerable<Message>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Messages.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Message message)
    {
        await _applicationDbContext.Messages.AddAsync(message);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<Message?> GetByMessageIdAsync(string messageId)
    {
        return await _applicationDbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
    }
}
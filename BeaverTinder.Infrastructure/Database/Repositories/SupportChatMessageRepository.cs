using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Infrastructure.Database.Contexts;

namespace BeaverTinder.Infrastructure.Database.Repositories;

public class SupportChatMessageRepository : ISupportChatMessageRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SupportChatMessageRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(SupportChatMessage message)
    {
        await _dbContext.SupportChatMessages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<SupportChatMessage> GetAll()
    {
        return  _dbContext.SupportChatMessages.AsQueryable();
    }
}
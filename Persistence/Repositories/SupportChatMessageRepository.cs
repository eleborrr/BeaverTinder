using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class SupportChatMessageRepository : ISupportChatMessageRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SupportChatMessageRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(SupportChatMessage message)
    {
        await _dbContext.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<SupportChatMessage> GetAll()
    {
        return  _dbContext.SupportChatMessages.AsQueryable();
    }
}
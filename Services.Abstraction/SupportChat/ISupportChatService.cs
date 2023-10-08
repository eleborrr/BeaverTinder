using Contracts;
using Domain.Entities;

namespace Services.Abstraction.SupportChat;

public interface ISupportChatService
{
    public Task SaveMessageAsync(SupportChatMessageDto message);
    public Task<IEnumerable<SupportChatMessageDto>> GetChatHistory(string userId, string secondUserId);
}
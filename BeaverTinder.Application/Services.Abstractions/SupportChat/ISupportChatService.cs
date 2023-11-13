using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Services.Abstractions.SupportChat;

public interface ISupportChatService
{
    public Task SaveMessageAsync(SupportChatMessageDto message);
    public Task<SupportRoom> GetChatById(string curUserId, string userId);
    public Task<IEnumerable<SupportRoom>> GetAllChatRooms();
    public Task<IEnumerable<SupportChatMessageDto>> GetChatHistory(string userId, string secondUserId);
}
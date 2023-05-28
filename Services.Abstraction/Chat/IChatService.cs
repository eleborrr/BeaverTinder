using Domain.Entities;

namespace Services.Abstraction.Chat;

public interface IChatService
{
    public Task<Room> GetChatById(string curUserId, string userId);
}
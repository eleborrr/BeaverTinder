using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Services.Abstractions.Chat;

public interface IChatService
{
    public Task<Room> GetChatById(string curUserId, string userId);
}
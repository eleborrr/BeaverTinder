using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Chat;

namespace Services.Chat;

public class ChatService: IChatService
{
    private readonly IRepositoryManager _repositoryManager;

    public ChatService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    
    
    public async Task<Room> GetChatById(string curUserId, string userId)
    {
        var room = (await _repositoryManager.RoomRepository.GetAllAsync(default))
            .FirstOrDefault(r => (r.FirstUserId == curUserId && r.SecondUserId == userId)
                ||
                (r.SecondUserId == curUserId && r.FirstUserId == userId));
        if (room is null)
        {
            room = new Room()
            {
                Id = Guid.NewGuid().ToString(),
                FirstUserId = curUserId,
                SecondUserId = userId,
                Name = Guid.NewGuid().ToString()
            };
            await _repositoryManager.RoomRepository.AddAsync(
                room);
        }

        return room;
    }
}
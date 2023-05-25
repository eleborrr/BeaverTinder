using Contracts.ViewModels;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories;
using Services.Abstraction.Chat;

namespace Services.Chat;

public class ChatService: IChatService
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;

    public ChatService(UserManager<User> userManager, IRepositoryManager repositoryManager)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
    }
    
    
    public async Task<Room> GetChatById(string curUserId, string userId)
    {
        var room = (await _repositoryManager.RoomRepository.GetAllAsync(default))
            .FirstOrDefault(r => (r.FirstUserId == curUserId || r.FirstUserId == userId)
                &&
                (r.SecondUserId == curUserId || r.SecondUserId == userId));
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
        // var model = new ChatModel
        // {
        //     UserId = curUserId,
        //     RecieverId = id,
        //     RoomName = room.Name
        // };
    }
}
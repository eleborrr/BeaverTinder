using Contracts;
using Domain.Entities;
using Domain.Repositories;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction.SupportChat;

namespace Services.SupportChat;

public class SupportChatService : ISupportChatService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IPublishEndpoint _publishEndpoint;

    public SupportChatService(IRepositoryManager repositoryManager, IPublishEndpoint publishEndpoint)
    {
        _repositoryManager = repositoryManager;
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task<SupportRoom> GetChatById(string curUserId, string userId)
    {
        var room = _repositoryManager.SupportRoomRepository.GetAll()
            .FirstOrDefault(r => (r.FirstUserId == curUserId && r.SecondUserId == userId)
                                 ||
                                 (r.SecondUserId == curUserId && r.FirstUserId == userId));
        if (room is null)
        {
            room = new SupportRoom()
            {
                FirstUserId = curUserId,
                SecondUserId = userId,
                Name = Guid.NewGuid().ToString()
            };
            await _repositoryManager.SupportRoomRepository.AddAsync(
                room);
        }
        return room;
    }
    
    public async Task<IEnumerable<SupportChatMessageDto>> GetChatHistory(string userId, string secondUserId)
    {
        var room = await GetChatById(userId, secondUserId);
        return room.Messages.Select(m => new SupportChatMessageDto()
        {
            Timestamp = m.Timestamp,
            Content = m.Content,
            RoomId = m.RoomId,
            ReceiverId = m.ReceiverId,
            SenderId = m.SenderId
        });
    }

    public async Task SaveMessageAsync(SupportChatMessageDto message)
    {
        await _publishEndpoint.Publish(message);
    }
}
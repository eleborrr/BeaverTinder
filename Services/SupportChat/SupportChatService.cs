using Contracts;
using Domain.Entities;
using Domain.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction.SupportChat;

namespace Services.SupportChat;

public class SupportChatService : ISupportChatService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly UserManager<User> _userManager;

    public SupportChatService(IRepositoryManager repositoryManager, IPublishEndpoint publishEndpoint, UserManager<User> userManager)
    {
        _repositoryManager = repositoryManager;
        _publishEndpoint = publishEndpoint;
        _userManager = userManager;
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
                Id = Guid.NewGuid().ToString(),
                FirstUserId = curUserId,
                SecondUserId = userId,
                Name = Guid.NewGuid().ToString(),
            };
            await _repositoryManager.SupportRoomRepository.AddAsync(
                room);
        }
        return room;
    }
    
    public async Task<IEnumerable<SupportChatMessageDto>> GetChatHistory(string userId, string secondUserId)
    {
        var room = await GetChatById(userId, secondUserId);
        var messages = _repositoryManager.SupportChatMessageRepository.GetAll().Where(msg => msg.RoomId == room.Id)
            .ToList();
        if (messages is null)
            return Array.Empty<SupportChatMessageDto>();
        var result = await Task.WhenAll(messages.Select(async m => new SupportChatMessageDto()
        {
            Timestamp = m.Timestamp,
            Content = m.Content,
            RoomId = m.RoomId,
            ReceiverId = m.ReceiverId,
            SenderId = m.SenderId,
            SenderName = (await _userManager.FindByIdAsync(m.SenderId)).UserName
        }));
        return result;
    }

    public async Task SaveMessageAsync(SupportChatMessageDto message)
    {
        var entity = new SupportChatMessage()
        {
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            Content = message.Content,
            Timestamp = message.Timestamp,
            RoomId = message.RoomId
        };
        await _publishEndpoint.Publish(entity);
    }
}
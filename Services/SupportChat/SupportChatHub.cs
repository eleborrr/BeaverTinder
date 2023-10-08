using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Persistence;

namespace Services.SupportChat;

public class SupportChatHub : Hub
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly ServiceManager _serviceManager;

    public SupportChatHub(ApplicationDbContext context, UserManager<User> userManager, ServiceManager serviceManager)
    {
        _dbContext = context;
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    public async Task SendPrivateMessage(string senderUserName, string receiverUserName, string message,
        string groupName)
    {
        var room = _dbContext.SupportRooms.FirstOrDefault(r => r.Name == groupName);

        var sender = await _userManager.FindByNameAsync(senderUserName);
        var receiver = await _userManager.FindByNameAsync(receiverUserName);
        
        var dto = new SupportChatMessageDto()
        {
            Content = message,
            RoomId = room.Id,
            SenderId = sender.Id,
            ReceiverId = receiver.Id,
            Timestamp = DateTime.Now
        };
        await _serviceManager.SupportChatService.SaveMessageAsync(dto);
        await Clients.Group(groupName).SendAsync("ReceivePrivateMessage", senderUserName, message);
    }


    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Persistence;
using Services.Abstraction;

namespace Services.SupportChat;

public class SupportChatHub : Hub
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;

    public SupportChatHub(ApplicationDbContext context, UserManager<User> userManager, IServiceManager serviceManager)
    {
        _dbContext = context;
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    public async Task SendPrivateMessage(
        string senderUserName,
        string message, 
        string receiverUserName,
        string groupName)
    {
        Console.WriteLine("-----> Message received");
        var room = _dbContext.SupportRooms.FirstOrDefault(r => r.Name == groupName);

        var sender = await _userManager.FindByNameAsync(senderUserName);
        var receiver = await _userManager.FindByNameAsync(receiverUserName);
        Console.WriteLine($"senderUserName: {senderUserName}\n" +
                          $"message: {message}\n" +
                          $"receiverUserName: {receiverUserName}\n" +
                          $"groupName: {groupName}\n");
        var dto = new SupportChatMessageDto()
        {
            Content = message,
            RoomId = room.Id,
            SenderId = sender.Id,
            ReceiverId = receiver.Id,
            Timestamp = DateTime.Now
        };
        await _serviceManager.SupportChatService.SaveMessageAsync(dto);
        await Clients.Group(groupName).SendAsync("Receive", senderUserName, message);
    }

    public async Task ConnectToRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
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
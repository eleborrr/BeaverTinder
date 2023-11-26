using Application.SupportChat.SaveMessageByDtoBus;
using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Infrastructure.Database;
using BeaverTinder.Shared.Files;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace BeaverTinder.API.Hubs;

public class SupportChatHub : Hub
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;

    public SupportChatHub(
        ApplicationDbContext context,
        UserManager<User> userManager,
        IMediator mediator)
    {
        _dbContext = context;
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task SendPrivateMessage(
        string senderUserName,
        string message,
        IEnumerable<FileModel> files,
        string receiverUserName,
        string groupName)
    {
        var room = _dbContext.SupportRooms.FirstOrDefault(r => r.Name == groupName);

        var sender = await _userManager.FindByNameAsync(senderUserName);
        var receiver = await _userManager.FindByNameAsync(receiverUserName);

        if (sender is null || receiver is null || room is null)
        {
            //TODO logger
            return;
        }
        
        var dto = new SupportChatMessageDto()
        {
            Content = message,
            RoomId = room.Id,
            SenderId = sender.Id,
            ReceiverId = receiver.Id,
            Timestamp = DateTime.Now
        };
        await _mediator.Send(new SaveMessageByDtoBusCommand(dto));
        await _mediator.Send(files);
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

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
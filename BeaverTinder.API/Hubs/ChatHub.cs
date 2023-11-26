using BeaverTinder.Domain.Entities;
using BeaverTinder.Infrastructure.Database;
using BeaverTinder.Shared.Files;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public ChatHub(ApplicationDbContext dbContext, UserManager<User> userManager, IMediator mediator)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mediator = mediator;
        }
        
        public async Task GetGroupMessages(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

            var room = _dbContext.Rooms.FirstOrDefault(r => r.Name == roomName);

            if (room is null)
                return;

            var messages = await _dbContext.Messages.Where(m => m.RoomId == room.Id).OrderBy(m => m.Timestamp).ToListAsync();
            
            foreach (var message in messages)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceivePrivateMessage", await GetUserName(message.SenderId), message.Content);
            }
        }

        private async Task<string?> GetUserName(string id)
        {
            return (await _userManager.FindByIdAsync(id))?.UserName;
        }

        public async Task SendPrivateMessage(string senderUserName, 
            string message,
            IEnumerable<FileModel> files,
            string receiverUserName,
            string groupName)
        {
            var room = _dbContext.Rooms.FirstOrDefault(r => r.Name == groupName);

            var sender = await _userManager.FindByNameAsync(senderUserName);
            var receiver = await _userManager.FindByNameAsync(receiverUserName);

            if (receiver is null || sender is null || room is null)
                return;
            
            _dbContext.Messages.Add(new Message()
            {
                Id = Guid.NewGuid().ToString(),
                Content = message,
                Timestamp = DateTime.Now,
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                RoomId = room.Id
            });
            await _dbContext.SaveChangesAsync();
            await _mediator.Send(files);
            await Clients.Group(groupName).SendAsync("ReceivePrivateMessage", senderUserName, message);
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
}
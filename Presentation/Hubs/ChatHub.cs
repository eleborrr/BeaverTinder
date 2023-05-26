using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Presentation.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task GetGroupMessages(string roomName, string senderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

            var room = _dbContext.Rooms.FirstOrDefault(r => r.Name == roomName);

            var messages = await _dbContext.Messages.Where(m => m.RoomId == room.Id).ToListAsync();
            
            foreach (var message in messages)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceivePrivateMessage", message.SenderId, message.Content);
                // await Clients.Client(senderId).SendAsync("ReceivePrivateMessage", message.SenderIdId, message.Content);
                // await Clients.Group(roomName)
                //     .SendAsync("ReceivePrivateMessage", message.SenderIdId, message.Content);
            }
        }

        public async Task SendPrivateMessage(string author, string message, string recieverId, string groupName)
        {
            var room = _dbContext.Rooms.FirstOrDefault(r => r.Name == groupName);
            
            _dbContext.Messages.Add(new Message()
            {
                Id = Guid.NewGuid().ToString(),
                Content = message,
                Timestamp = DateTime.Today,
                SenderId = author,
                ReceiverId = recieverId,
                RoomId = room.Id
            });
            await _dbContext.SaveChangesAsync();
            await Clients.Group(groupName).SendAsync("ReceivePrivateMessage", author, message);
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
}
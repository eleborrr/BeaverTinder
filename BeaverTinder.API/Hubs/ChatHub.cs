using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Application.Features.Chat.SaveMessage;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Infrastructure.Database;
using BeaverTinder.Shared.Files;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IBus _bus;
        private readonly IMediator _mediator;

        public ChatHub(ApplicationDbContext dbContext, UserManager<User> userManager, IMediator mediator, IBus bus)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mediator = mediator;
            _bus = bus;
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
                var files = _dbContext.Files
                    .Where(f => f.MessageId == message.Id)
                    .Select(msg => msg.FileGuidName)
                    .ToList();
                await Clients.Client(Context.ConnectionId).SendAsync("ReceivePrivateMessage", 
                    await GetUserName(message.SenderId), 
                    message.Content,
                    files);
            }
        }

        private async Task<string?> GetUserName(string id)
        {
            return (await _userManager.FindByIdAsync(id))?.UserName;
        }
        
        public async Task SendPrivateMessage(string senderUserName, 
            string message,
            [FromForm] FileModelReceive[]  files,
            string receiverUserName,
            string groupName)
        {
            if(files.Length == 0 )
                return;
            Console.WriteLine("Joined sendprivatemessage");
            var room = _dbContext.Rooms.FirstOrDefault(r => r.Name == groupName);
            

            var sender = await _userManager.FindByNameAsync(senderUserName);
            var receiver = await _userManager.FindByNameAsync(receiverUserName);

            Console.WriteLine(senderUserName);
            Console.WriteLine(message);
            Console.WriteLine(files);
            Console.WriteLine(receiverUserName);
            Console.WriteLine(groupName);
            if (receiver is null || sender is null || room is null)
                return;

            var newMessage = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Content = message,
                Timestamp = DateTime.Now,
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                RoomId = room.Id
            };
            
            _dbContext.Messages.Add(newMessage);
            foreach (var file in files)
            {
                var fileArr = new SaveFileMessage
                    (file.BytesArray.Select(x => (byte)x).ToArray(), Guid.NewGuid().ToString(), "my-bucket");
                _dbContext.Files.Add(new FileToMessage
                {
                    FileGuidName = fileArr.FileIdentifier + ".txt",
                    MessageId = newMessage.Id
                });
            }
            
            await _dbContext.SaveChangesAsync();
            
            var dto = new ChatMessageDto()
            {
                Content = message,
                RoomId = room.Id,
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Timestamp = DateTime.Now
            };
            await _mediator.Send(new SaveChatMessageByDtoBusCommand(dto));
            
            if (files.Length > 0)
                await _bus.Publish(new FileMessage
                    (files
                        .Select(x => x.BytesArray)
                        .Select(x => 
                            new FileModelSend(x.Select(y => (byte)y).ToArray()))
                    .ToArray(), Guid.NewGuid().ToString(), "mybucket"));
            // await Clients.Group(groupName).SendAsync("ReceivePrivateMessage", senderUserName, 
            //     message, new List<string>{fileArr.FileIdentifier});
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
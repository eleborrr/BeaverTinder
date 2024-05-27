using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Application.Features.SupportChat.SaveMessageByDtoBus;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Infrastructure.Database;
using BeaverTinder.Shared;
using BeaverTinder.Shared.Files;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Empty = BeaverTinder.Shared.Empty;
namespace BeaverTinder.SupportChat.Services;

public class SupportChatRpcService : Chat.ChatBase
{
    private readonly ISupportChatRoomService _chatRoomService;
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;

    public SupportChatRpcService(ISupportChatRoomService chatRoomService, IMediator mediator, UserManager<User> userManager, ApplicationDbContext dbContext)
    {
        _chatRoomService = chatRoomService;
        _mediator = mediator;
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public override async Task ConnectToRoom(JoinRequest request, IServerStreamWriter<MessageGrpc> responseStream, ServerCallContext context)
    {
        
        var client = new ChatClient
        {
            StreamWriter = responseStream,
            UserName = request.UserName
        };
        
        await _chatRoomService.AddClientToChatRoom(request.RoomName, client);
        
        while (!context.CancellationToken.IsCancellationRequested)
        {
        }
        
        await _chatRoomService.RemoveClientFromChatRoom(request.RoomName, client);
        
    }

    public override async Task<Empty> SendMessage(MessageGrpc request, ServerCallContext context)
    {
        var room = _dbContext.SupportRooms.FirstOrDefault(r => r.Name == request.GroupName);
    
        var sender = await _userManager.FindByNameAsync(request.UserName);
        var receiver = await _userManager.FindByNameAsync(request.ReceiverUserName);

        var files = new List<SaveFileMessage>();
        

        foreach (var file in request.Files)
        {
            var fileData = new FileData(file.Content.ToByteArray());
            var metaData = new Dictionary<string, string>(file.MetaData);
            files.Add(new SaveFileMessage(fileData,metaData, file.FileName, file.MainBucketIdentifier, file.TemporaryBucketIdentifier));
        }
        
        if (sender is null || receiver is null || room is null)
        {
            return await base.SendMessage(request, context);
        }
        var dto = new ChatMessageDto()
        {
            Content = request.Message,
            RoomId = room.Id,
            SenderId = sender.Id,
            ReceiverId = receiver.Id,
            Timestamp = DateTime.Now
        };
        
        await _mediator.Send(new SaveMessageByDtoBusCommand(dto));
        
        // await _mediator.Send(files);
        
        await _chatRoomService.BroadcastMessageToChatRoom(request);
        return new Empty();
    }
}
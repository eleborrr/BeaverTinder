using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Application.Features.SupportChat.SaveMessageByDtoBus;
using BeaverTinder.Mobile.Helpers;
using BeaverTinder.Mobile.Helpers.Jwt;
using BeaverTinder.Shared;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace BeaverTinder.Mobile.Services;

public class ChatService: Chat.ChatBase
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMediator _mediator;

    public ChatService(IServiceScopeFactory scopeFactory, IMediator mediator)
    {
        _scopeFactory = scopeFactory;
        _mediator = mediator;
    }

    [Authorize]
    public override async Task ReceiveMsg(Empty request, IServerStreamWriter<Msg> responseStream, ServerCallContext context)
    {
        var userId = await GetUserId(context);
        Connections.connections[userId] = responseStream;
        await Task.Delay(Timeout.Infinite);
    }

    [Authorize]
    public override async Task<Empty> SendMsg(Msg request, ServerCallContext context)
    {
        var userId = await GetUserId(context);
        var subscriber = Connections.connections[userId];
        
        var dto = new ChatMessageDto()  // валидация данных??
        {
            Content = request.Content,
            RoomId = request.Room,
            SenderId = userId,
            ReceiverId = request.To,
            Timestamp = DateTime.Parse(request.Time)
        };
        await _mediator.Send(new SaveMessageByDtoBusCommand(dto)); // проверка результата?
        await subscriber.WriteAsync(request);
        return new Empty();
    }

    private async Task<string> GetUserId(ServerCallContext context)
    {
        var scope = _scopeFactory.CreateScope();
        var claimsPrincipal = context.GetHttpContext().User;
        var user = await JwtHelper.GetUserFromJwt(claimsPrincipal, scope);
        return user.Id;
    }
}
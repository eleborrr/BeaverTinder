using System.Security.Claims;
using BeaverTinder.Application.Dto.Chat;
using BeaverTinder.Application.Dto.ResponsesAbstraction;
using BeaverTinder.Application.Features.Chat.AddChat;
using BeaverTinder.Application.Features.Chat.GetChatById;
using BeaverTinder.Application.Features.Like.GetIsMutualSympathy;
using BeaverTinder.Domain.Entities;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Identity;
using IMediator = MediatR.IMediator;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{
    public async Task<IEnumerable<AllChatsResponse>> Chats(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
    {
        try
        {
            var scope = _scopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var curUser = await GetUserFromJwt(claimsPrincipal, scope);
            if (curUser is null)
                throw new ArgumentNullException("user doesn't exists");
            
            var users = userManager.Users.AsEnumerable()
                .Where(u => mediator.Send(
                    new GetIsMutualSympathyQuery(curUser, u),
                    cancellationToken).Result.Value);  //_serviceManager.LikeService.IsMutualSympathy(curUser, u).Result
            var model = users.Select(x => new AllChatsResponse
            {
                UserName = x.UserName!,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Image = x.Image!
            });
            return model;
        }
        catch (Exception exception)
        {
            return new List<AllChatsResponse>() {new() {FirstName = exception.Message}};
        }
    }
    
    public async Task<SingleChatGetResponse> Chat(string username, ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
    {
        try
        {
            var scope = _scopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var sender = await GetUserFromJwt(claimsPrincipal, scope);
            var receiver = await userManager.FindByNameAsync(username);

            if (receiver is null || sender is null)
                throw new ArgumentNullException("Ooops!");
            var res = await mediator.Send(new GetChatByIdQuery(sender.Id, receiver.Id), cancellationToken);

            if (res is { IsSuccess: false, Error: "Room not found" })
            {
                res = (await mediator.Send(new AddChatCommand(sender.Id, receiver.Id), cancellationToken))!;
            }

            if (!res.IsSuccess)
                return new SingleChatGetResponse() { ReceiverName = "Wrong data!" };
                //return Json(new FailResponse(false, "Wrong data!", 400));
            var model = new SingleChatGetResponse
            {
                SenderName = sender.UserName!,
                ReceiverName = username,
                RoomName = res.Value!.Name
            };
            return model;
        }
        catch (Exception exception)
        {
            return new SingleChatGetResponse() { ReceiverName = exception.Message };
            //return Json(new FailResponse(false, exception.Message, 400));
        }
    }
}
using BeaverTinder.Application.Dto.Chat;
using BeaverTinder.Application.Dto.ResponsesAbstraction;
using BeaverTinder.Application.Features.Chat.AddChat;
using BeaverTinder.Application.Features.Chat.GetChatById;
using BeaverTinder.Application.Features.Like.GetIsMutualSympathy;
using BeaverTinder.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BeaverTinder.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class ChatController: Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;

    public ChatController(UserManager<User> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    [HttpGet("/im")]
    public async Task<JsonResult> Chats(CancellationToken cancellationToken)
    {
        try
        {
            var s = User.Claims.FirstOrDefault(c => c.Type == "Id")!;
            var curUser = await _userManager.FindByIdAsync(s.Value);

            if (curUser is null)
                throw new ArgumentNullException("user doesn't exists");
            
            var users = _userManager.Users.AsEnumerable()
                .Where(u => _mediator.Send(
                    new GetIsMutualSympathyQuery(curUser, u),
                    cancellationToken).Result.Value);  //_serviceManager.LikeService.IsMutualSympathy(curUser, u).Result
            var model = users.Select(x => new AllChatsResponse
            {
                UserName = x.UserName!,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Image = x.Image!
            });
            return Json(model);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse(false, exception.Message, 400));
        }
    }
    
    [HttpGet("/im/chat")]
    public async Task<JsonResult> Chat([FromQuery] string username, CancellationToken cancellationToken)
    {
        try
        {
            var curUserId = User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            var receiver = await _userManager.FindByNameAsync(username);
            var sender = await _userManager.FindByIdAsync(curUserId);

            if (receiver is null || sender is null)
                throw new ArgumentNullException("Ooops!");
            var res = await _mediator.Send(new GetChatByIdQuery(sender.Id, receiver.Id), cancellationToken);

            if (res is { IsSuccess: false, Error: "Room not found" })
            {
                res = (await _mediator.Send(new AddChatCommand(sender.Id, receiver.Id), cancellationToken))!;
            }
            if (!res.IsSuccess)
                return Json(new FailResponse(false, "Wrong data!", 400));
            var model = new SingleChatGetResponse
            {
                SenderName = sender.UserName!,
                ReceiverName = username,
                RoomName = res.Value!.Name
            };
            return Json(model);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse(false, exception.Message, 400));
        }
    }
}
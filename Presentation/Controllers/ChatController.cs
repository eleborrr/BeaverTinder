using Contracts.Responses;
using Contracts.Responses.Chat;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class ChatController: Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;

    public ChatController(UserManager<User> userManager, IServiceManager serviceManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    [HttpGet("/im")]
    public async Task<JsonResult> Chats()
    {
        try
        {
            // var users = await _userManager.Users.ToListAsync();

            var s = User.Claims.FirstOrDefault(c => c.Type == "Id")!;
            var curUser = await _userManager.FindByIdAsync(s.Value);

            if (curUser is null) //TODO add log
                throw new Exception("Oops!");
            
            var users = _userManager.Users.AsEnumerable()
                .Where(u => _serviceManager.LikeService.IsMutualSympathy(curUser, u).Result);
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
    public async Task<JsonResult> Chat([FromQuery] string username)
    {
        try
        {
            var curUserId = User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            var receiver = await _userManager.FindByNameAsync(username);
            var sender = await _userManager.FindByIdAsync(curUserId);

            if (receiver is null || sender is null) //TODO add log
                throw new Exception("Oops!");
            
            var res = await _serviceManager.ChatService.GetChatById(sender.Id, receiver.Id);
            var model = new SingleChatGetResponse()
            {
                SenderName = sender.UserName!,
                ReceiverName = username,
                RoomName = res.Name
            };
            return Json(model);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse(false, exception.Message, 400));
        }
    }
}
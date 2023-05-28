using System.Security.Claims;
using Contracts.Responses;
using Contracts.Responses.Chat;
using Contracts.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services.Abstraction;
using Services.Abstraction.Chat;

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
            var users = await _userManager.Users
                .Where(u => _serviceManager.LikeService.).ToListAsync();
            var model = users.Select(x =>
            {
                return new AllChatsResponse
                {
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Image = x.Image
                };
            });
            return Json(model);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse()
            {
                Message = exception.Message,
                StatusCode = 400,
                Successful = false
            });
        }
    }
    
    [HttpGet("/im/chat")]
    public async Task<JsonResult> Chat([FromQuery] string username)
    {
        try
        {
            var curUserId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            var receiver = await _userManager.FindByNameAsync(username);
            var sender = await _userManager.FindByIdAsync(curUserId);

            //TODO check for curUserId null
            var res = await _serviceManager.ChatService.GetChatById(sender.Id, receiver.Id);
            var model = new SingleChatGetResponse()
            {
                RecieverName = username,
                SenderName = sender.UserName,
                RoomName = res.Name
            };
            return Json(model);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse()
            {
                Message = exception.Message,
                StatusCode = 400,
                Successful = false
            });
        }
    }
}
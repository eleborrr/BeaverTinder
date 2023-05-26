using System.Security.Claims;
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
        var users = await _userManager.Users.ToListAsync();
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
    
    [HttpGet("/im/chat")]
    public async Task<JsonResult> Chat([FromQuery] string id)
    {
        var curUserId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
        
        //TODO check for curUserId null
        var res = await _serviceManager.ChatService.GetChatById(curUserId, id);
        return Json(res);
    }
}
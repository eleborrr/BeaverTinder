using Contracts.Responses;
using Contracts.Responses.Chat;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;


namespace Presentation.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class SupportChatController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly UserManager<User> _userManager;

    public SupportChatController(IServiceManager serviceManager, UserManager<User> userManager)
    {
        _serviceManager = serviceManager;
        _userManager = userManager;
    }
    
    [HttpGet("/im/supportChat")]
    public async Task<JsonResult> Chat([FromQuery] string username)
    {
        try
        {
            var curUserId = User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            var receiver = await _userManager.FindByNameAsync(username);
            var sender = await _userManager.FindByIdAsync(curUserId);

            //TODO check for curUserId null
            var res = await _serviceManager.SupportChatService.GetChatById(sender!.Id, receiver!.Id);
            var model = new SingleChatGetResponse()
            {
                ReceiverName = username,
                SenderName = sender.UserName!,
                RoomName = res.Name
            };
            return Json(model);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse(false, exception.Message, 400));
        }
    }
    
    [HttpGet("/history")]
    public async Task<JsonResult> GetChatHistory([FromQuery] string username)
    {
        var claim = User.Claims.FirstOrDefault(c => c.Type == "Id");
        var user = await _userManager.FindByIdAsync(claim!.Value);
        var secondUser = await _userManager.FindByNameAsync(username);
        var history = await _serviceManager.SupportChatService.GetChatHistory(user!.Id, secondUser!.Id);
        return Json(history);
    }
}
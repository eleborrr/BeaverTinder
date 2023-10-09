using Contracts;
using Contracts.Responses;
using Contracts.Responses.Chat;
using Domain.Entities;
using Domain.Repositories;
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
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;

    public SupportChatController(IRepositoryManager repositoryManager, IServiceManager serviceManager, UserManager<User> userManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }
    
    [HttpGet("/im/supportChat")]
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
            return Json(new FailResponse(false, exception.Message, 400));
        }
    }
    
    [HttpGet("/history")]
    public async Task<JsonResult> GetChatHistory([FromQuery] string username)
    {
        var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
        var secondUser = await _userManager.FindByNameAsync(username);
        var history = await _serviceManager.SupportChatService.GetChatHistory(user!.Id, secondUser!.Id);
        return Json(history);
    }

    [HttpGet("send")]
    public async Task<IActionResult> SendMessage([FromQuery] string message)
    {
        var m = new SupportChatMessageDto()
        {
            Content = message,
            ReceiverId = "1",
            RoomId = "23",
            SenderId = "43",
            Timestamp = DateTime.Now,
        };
        await _serviceManager.SupportChatService.SaveMessageAsync(m);
        return Ok(m);
    }
}
using Application.SupportChat.GetChatById;
using Application.SupportChat.GetSupportChatHistory;
using Contracts.Dto.Chat;
using Contracts.ResponsesAbstraction;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace API.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]")]
public class SupportChatController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;

    public SupportChatController(IServiceManager serviceManager, UserManager<User> userManager, IMediator mediator)
    {
        _serviceManager = serviceManager;
        _userManager = userManager;
        _mediator = mediator;
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
            var res = (await _mediator.Send(new GetSupportChatByIdQuery(sender!.Id, receiver!.Id))).Value;
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
    public async Task<JsonResult> GetChatHistory([FromQuery] string username, CancellationToken cancellationToken)
    {
        var claim = User.Claims.FirstOrDefault(c => c.Type == "Id");
        var user = await _userManager.FindByIdAsync(claim!.Value);
        var secondUser = await _userManager.FindByNameAsync(username);

        var chatRoom = (await _mediator.Send(new GetSupportChatByIdQuery(user!.Id, secondUser!.Id), cancellationToken)).Value;
        var history = (await _mediator.Send(new GetSupportChatHistoryByIdRoomQuery(chatRoom.Id))).Value;
        return Json(history);
    }
}
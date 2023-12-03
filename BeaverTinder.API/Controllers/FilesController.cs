using BeaverTinder.Application.Dto.Chat;
using BeaverTinder.Application.Dto.ResponsesAbstraction;
using BeaverTinder.Application.Features.Chat.AddChat;
using BeaverTinder.Application.Features.Chat.GetChatById;
using BeaverTinder.Application.Features.Like.GetIsMutualSympathy;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Shared.Files;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class FilesController: Controller
{
    private readonly IBus _bus;

    public FilesController(UserManager<User> userManager, IMediator mediator, IBus bus)
    {
        _bus = bus;
    }

    [HttpPost("/uploadFile")]
    public async Task<JsonResult> UploadFile([FromBody] IFormFileCollection fileInput)
    {
        try
        {
            var result = new List<string>();
            foreach (var file in fileInput)
            {
                var fileDto = new SaveFileMessage
                    (file, Guid.NewGuid().ToString(), "my-bucket");
               
                result.Add(fileDto.FileName);
            
                if (file.Length > 0)
                    await _bus.Publish(fileDto);
            }
            
            return Json(result);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse(false, exception.Message, 400));
        }
    }
}
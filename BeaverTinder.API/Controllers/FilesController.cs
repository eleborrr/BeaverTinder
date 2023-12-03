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
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;
    private readonly IBus _bus;

    public FilesController(UserManager<User> userManager, IMediator mediator, IBus bus)
    {
        _userManager = userManager;
        _mediator = mediator;
        _bus = bus;
    }

    [HttpPost("/uploadFile")]
    public async Task<JsonResult> UploadFile([FromBody] IFormFile fileInput)
    {
        try
        {
            var file = new SaveFileMessage
                (fileInput, Guid.NewGuid().ToString(), "my-bucket");
            _dbContext.Files.Add(new FileToMessage
            {
                FileGuidName = file.FileIdentifier + ".txt",
                MessageId = newMessage.Id
            });
            await _dbContext.SaveChangesAsync();
            
            if (fileInput.Length > 0)
                await _bus.Publish(file);
            
            return Json(file.FileIdentifier);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse(false, exception.Message, 400));
        }
    }
}
using Application.SupportChat.GetAllSupportChatRooms;
using Contracts.Dto.AdminPage;
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
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdminController: Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;
    private readonly IMediator _mediator;
    private const bool ModeratorReturnValue = true;
    
    public AdminController(UserManager<User> userManager, IServiceManager serviceManager, IMediator mediator)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
        _mediator = mediator;
    }

    [Authorize(Policy = "OnlyForModerators")]
    [HttpGet("/ban")]
    public IActionResult BanUser()
    {
        return Ok();
    }
    
    [Authorize(Policy = "OnlyForAdmins")]
    [HttpPost("/ban")]
    public async Task<IActionResult> BanUser([FromBody] AdminPageUserIdDto adminPageUserId)  //List<User>
    {
        var user = await _userManager.FindByIdAsync(adminPageUserId.UserId);
        
        if (user == null)
        {
            return NotFound();
        }
        
        user.IsBlocked = true;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return RedirectToAction("GetAllUsers", "Account");
    }
    
    [Authorize(Policy = "OnlyForAdmins")]
    [HttpPost("/unban")]
    public async Task<IActionResult> UnbanUser([FromBody] AdminPageUserIdDto adminPageUserId)  //List<User>
    {
        var user = await _userManager.FindByIdAsync(adminPageUserId.UserId);
        
        if (user == null)
        {
            return NotFound();
        }
        
        user.IsBlocked = false;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return RedirectToAction("GetAllUsers", "Account");
    }
    
    [Authorize(Policy = "OnlyForAdmins")]
    [HttpPost("/deactivate")]
    public async Task<IActionResult> DeactivateSearch([FromBody] AdminPageUserIdDto userIdId)  //List<User>
    {
        var user = await _userManager.FindByIdAsync(userIdId.UserId);
        
        if (user == null)
        {
            return NotFound();
        }
        
        user.IsSearching = false;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return RedirectToAction("GetAllUsers", "Account");
    }
    
    [HttpGet("/supportChats")]
    public async Task<JsonResult> Chats()
    {
        try
        {
            var curUserId = User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            var chats = (await _mediator.Send(new GetAllSupportChatRoomsQuery())).Value!.ToList();
            var model = chats.Select(x =>
            {
                var user = _userManager.FindByIdAsync(x.FirstUserId != curUserId? x.FirstUserId: x.SecondUserId).Result;

                if (user is null)
                    throw new SystemException("data about user is missing");
                
                return new AllChatsResponse
                {
                    UserName = user.UserName!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Image = user.Image!
                };
            }).ToList();
            return Json(model);
        }
        catch (Exception exception)
        {
            return Json(new FailResponse(false, exception.Message, 400));
        }
    }
    
    [Authorize(Policy = "OnlyForModerators")]
    [HttpPost("/activate")]
    public async Task<IActionResult> ActivateSearch([FromBody] AdminPageUserIdDto userIdId)  //List<User>
    {
        var user = await _userManager.FindByIdAsync(userIdId.UserId);
        
        if (user == null)
        {
            return NotFound();
        }
        
        user.IsSearching = true;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return RedirectToAction("GetAllUsers", "Account");
    }
    
    [Authorize(Policy = "OnlyForAdmins")]
    [HttpPost("/add_moderator")]
    public async Task<IActionResult> AddModerator([FromBody] AdminPageUserIdDto userIdId)
    {
        var user = await _userManager.FindByIdAsync(userIdId.UserId);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.AddToRoleAsync(user, "Moderator");
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
            // обработка ошибок добавления роли
        }

        return RedirectToAction("GetAllUsers", "Account");
    }
    
    [Authorize(Policy = "OnlyForAdmins")]
    [HttpPost("/add_admin")]
    public async Task<IActionResult> AddAdmin([FromBody] AdminPageUserIdDto userIdId)
    {
        var user = await _userManager.FindByIdAsync(userIdId.UserId);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.AddToRoleAsync(user, "Admin");
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
            // обработка ошибок добавления роли
        }

        return RedirectToAction("GetAllUsers", "Account");
    }

    [Authorize(Policy = "OnlyForModerators")]
    [HttpGet("page")]
    public bool GetAdminPage()
    {
        return ModeratorReturnValue;
    }
    
}
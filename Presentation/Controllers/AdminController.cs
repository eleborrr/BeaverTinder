using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController: Controller
{
    private readonly UserManager<User> _userManager;
    
    public AdminController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [Authorize(Policy = "OnlyForAdmins")]
    [HttpGet("/ban")]
    public IActionResult BanUser()
    {
        return Ok();
    }
    
    [Authorize(Policy = "OnlyForAdmins")]
    [HttpPost("/ban")]
    public async Task<IActionResult> BanUser([FromBody] AdminPageUserIdDto adminPageUserId)  //List<User>
    {
        var user = await _userManager.FindByNameAsync(adminPageUserId.UserId);
        
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
    
    [Authorize(Policy = "OnlyForAdmins")]
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

    //[Authorize(Policy = "OnlyForAdmins")]
    [Authorize]
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
    [HttpGet("page")]
    public bool GetAdminPage()
    {
        return true;
    }
    
}
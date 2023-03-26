using BeaverTinder.Models;
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
        return View("Admin");
    }
    
    [Authorize(Policy = "OnlyForAdmins")]
    [HttpPost("/ban")]
    public async Task<IActionResult> BanUser([FromBody] BanUserViewModel banUser)  //List<User>
    {
        var user = await _userManager.FindByNameAsync(banUser.UserName);
        
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
    [Authorize]
    [HttpPost("/unban")]
    public async Task<IActionResult> UnBanUser([FromBody] BanUserViewModel banUser)  //List<User>
    {
        var user = await _userManager.FindByNameAsync(banUser.UserName);
        
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
    
    //[Authorize(Policy = "OnlyForAdmins")]
    [Authorize]
    [HttpPost("/deactivate")]
    public async Task<IActionResult> DeactivateSearch(string userId)  //List<User>
    {
        var user = await _userManager.FindByIdAsync(userId);
        
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

    //[Authorize(Policy = "OnlyForAdmins")]
    [Authorize]
    [HttpPost("/add_moderator")]
    public async Task<IActionResult> AddModerator(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
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
}
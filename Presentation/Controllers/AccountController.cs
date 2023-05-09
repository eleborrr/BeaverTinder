using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Services.Abstraction.Geolocation;
using Services.Abstraction.TwoFA;

namespace Presentation.Controllers;


//TODO методы для изменения информации об аккаунте


[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;
    private readonly IGeolocationService _geolocationService;
    
    public AccountController(IServiceManager serviceManager, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    
    //TODO исправить юрл
    [HttpGet("/geolocation")] 
    public async Task<UserGeolocation> GetUserGeolocation([FromQuery] string userId)
    {
        return await _geolocationService.GetByUserId(userId);
    }
    
    [HttpGet("/all")]
    public List<User> GetAllUsers()
    {
        return _userManager.Users.ToList();
    }

    [HttpGet("/empty")]
    [Authorize]
    public IActionResult EmptyPage()
    {
        return Ok("empty");
    }

    [HttpGet("/confirm")]
    [AllowAnonymous]
    public async Task<JsonResult> ConfirmEmail([FromQuery] string userEmail, [FromQuery] string token)
    {
        if (userEmail == null || token == null)
        {

        }

        var res = await _serviceManager.AccountService.ConfirmEmailAsync(userEmail, token);
        return Json(res);
    }
    
    [HttpGet("/resetPassword")]
    [AllowAnonymous]
    public async Task<JsonResult> ResetPassword([FromQuery] string userEmail, [FromQuery] string token)
    {
        //TODO где получать пароль?
        
        if (userEmail == null || token == null)
        {

        }

        var res = await _serviceManager.AccountService.ResetPasswordAsync(userEmail, token, "123");
        return Json(res);
    }
}
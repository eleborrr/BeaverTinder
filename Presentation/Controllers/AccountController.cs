using System.Security.Claims;
using Contracts;
using Contracts.ViewModels;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction;
using Services.Abstraction.Geolocation;
using Services.Abstraction.TwoFA;

namespace Presentation.Controllers;


//TODO методы для изменения информации об аккаунте

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;
    
    public AccountController(IServiceManager serviceManager, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }
    
    [Authorize(Policy = "OnlyMapSubs")]
    [HttpPost("/geolocation")]
    public async Task<UserGeolocation> GetUserGeolocation([FromBody] GeolocationRequestViewModel model)
    {
        return await _serviceManager.GeolocationService.GetByUserId(model.UserId);
    }

    //TODO уменьшить количество выдаваемых данных
    [HttpGet("/userinfo")]
    public async Task<JsonResult> GetAccountInformation([FromQuery] string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        var geoloc = await _serviceManager.GeolocationService.GetByUserId(id);
        var model = new EditUserViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Image = user.Image,
            About = user.About,
            Gender = user.Gender,
            Latitude = geoloc.Latitude,
            Longitude = geoloc.Longtitude
        };
        return Json(model);
    }

    [HttpPost("/edit")]
    public async Task<JsonResult> EditAccount([FromBody] EditUserDto model)
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == "Id");
        var user = await _userManager.FindByIdAsync(s.Value);
        var b = await _serviceManager.AccountService.EditAccount(user, model, ModelState);
        return Json(b);
    }
    
    [HttpGet("/all")]
    public async Task<JsonResult> GetAllUsers()
    {
        return Json(await _userManager.Users.ToListAsync());
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
    
    // [HttpGet("/resetPassword")]
    // [AllowAnonymous]
    // public async Task<JsonResult> ResetPassword([FromQuery] string userEmail, [FromQuery] string token)
    // {
    //     //TODO где получать пароль?
    //     
    //     if (userEmail == null || token == null)
    //     {
    //
    //     }
    //
    //     var res = await _serviceManager.AccountService.ResetPasswordAsync(userEmail, token, "123");
    //     return Json(res);
    // }
}
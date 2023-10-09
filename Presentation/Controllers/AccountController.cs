using Contracts;
using Contracts.Responses;
using Contracts.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;
    
    public AccountController(IServiceManager serviceManager, UserManager<User> userManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }
    
    [Authorize(Policy = "OnlyMapSubs")]
    [HttpPost("/geolocation")]
    public async Task<UserGeolocation?> GetUserGeolocation([FromBody] GeolocationRequestViewModel model)
    {
        return await _serviceManager.GeolocationService.GetByUserId(model.UserId);
    }

    [HttpGet("/userinfo")]
    public async Task<JsonResult> GetAccountInformation([FromQuery] string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return new JsonResult(new FailResponse(false, "User not found", 404));
        
        var geolocation = await _serviceManager.GeolocationService.GetByUserId(id);
        if (geolocation is null)
            return new JsonResult(new FailResponse(false, "Oops! Seems like a problem.. We are working on it!", 400));
        
        var subInfo = await _serviceManager.SubscriptionService.GetUserActiveSubscription(id);
        var model = new EditUserViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName!,
            Image = user.Image!,
            About = user.About ?? "",
            Gender = user.Gender,
            Latitude = geolocation.Latitude,
            Longitude = geolocation.Longitude,
            SubName = subInfo.Name,
            SubExpiresDateTime = subInfo.Expires
        };
        return Json(model);
    }

    [HttpGet("/usersubinfo")]
    public async Task<JsonResult> GetUserSubInformation([FromQuery] string userId)
    {
        var subInfo = await _serviceManager.SubscriptionService.GetUserActiveSubscription(userId);

        var model = new SubInfoDto()
        {
            Name = subInfo.Name,
            Expires = subInfo.Expires
        };
        return Json(model);
    }

    [HttpPost("/edit")]
    public async Task<JsonResult> EditAccount([FromBody] EditUserDto model)
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == "Id")!;
        var user = await _userManager.FindByIdAsync(s.Value);

        if (user is null)
            return new JsonResult(new FailResponse(
                false,
                "User not found",
                404));
        
        var b = await _serviceManager.AccountService.EditAccount(user, model, ModelState);
        return Json(b);
    }
    
    [Authorize(Policy = "OnlyForModerators")]
    [HttpGet("/all")]
    public async Task<JsonResult> GetAllUsers()
    {
        var users = _userManager.Users;
        
        var result = new List<AdminUserDto>();
        foreach (var user in users)
        {
            var subscription = await _serviceManager.SubscriptionService.GetUserActiveSubscription(user.Id);
            result.Add(new ()
            {
                UserName = user.UserName!,
                SubName = subscription.Name,
                SubExpiresDateTime = subscription.Expires,
                Id = user.Id,
                IsBlocked = user.IsBlocked,
                IsSearching = user.IsSearching,
                
            });
        }
        return Json(result);
    }

    [HttpGet("/confirm")]
    [AllowAnonymous]
    public async Task<JsonResult> ConfirmEmail([FromQuery] string? userEmail, [FromQuery] string? token)
    {
        var res = await _serviceManager.AccountService.ConfirmEmailAsync(userEmail, token);
        return Json(res);
    }
}
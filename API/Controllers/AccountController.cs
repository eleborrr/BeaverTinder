using Application.Subscription.GetUsersActiveSubscription;
using Contracts.Dto.Account;
using Contracts.Dto.AdminPage;
using Contracts.Dto.Geolocation;
using Contracts.Dto.Subscription;
using Contracts.ResponsesAbstraction;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;
    private readonly IMediator _mediator;
    
    public AccountController(IServiceManager serviceManager, UserManager<User> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
        _serviceManager = serviceManager;
    }
    
    [Authorize(Policy = "OnlyMapSubs")]
    [HttpPost("/geolocation")]
    public async Task<UserGeolocation?> GetUserGeolocation([FromBody] GetGeolocationRequestDto model)
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

        var subInfo = (await _mediator.Send(new GetUsersActiveSubscriptionQuery(id))).Value;
        var model = new EditUserRequestDto
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
        var subInfo = (await _mediator.Send(new GetUsersActiveSubscriptionQuery(userId))).Value;
        
        var model = new SubscriptionInfoDto()
        {
            Name = subInfo.Name,
            Expires = subInfo.Expires
        };
        return Json(model);
    }

    [HttpPost("/edit")]
    public async Task<JsonResult> EditAccount([FromBody] EditUserRequestDto model)
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
            var subscription = (await _mediator.Send(new GetUsersActiveSubscriptionQuery(user.Id))).Value;
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
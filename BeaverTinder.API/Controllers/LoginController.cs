using System.Globalization;
using Application.OAuth.AddUserToVk;
using Application.OAuth.GetUserFromToVkById;
using Application.OAuth.GetVkUserInfo;
using Application.OAuth.Login;
using Application.OAuth.Register;
using AspNet.Security.OAuth.Vkontakte;
using BeaverTinder.Application.Dto.Authentication.Login;
using BeaverTinder.Application.Dto.Vk;
using BeaverTinder.Application.Features.OAuth.GetUserFromToVkById;
using BeaverTinder.Application.Features.OAuth.GetVkUserInfo;
using BeaverTinder.Application.Features.OAuth.Login;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BeaverTinder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _config;
    private readonly HttpClient _client;
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;

    public LoginController(
        IServiceManager serviceManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        HttpClient client,
        IMediator mediator,
        UserManager<User> userManager)
    {
        _serviceManager = serviceManager;
        _signInManager = signInManager;
        _config = configuration;
        _client = client;
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<JsonResult> Login([FromBody] LoginRequestDto model)
    {
        return Json(await _serviceManager.AccountService.Login(model, ModelState));
    }
    
    [HttpGet("getAccessToken")]
    public async Task<IActionResult> GetAccessToken(
        [FromQuery] string code, 
        CancellationToken cancellationToken)
    {
        var query = new Dictionary<string, string?>
        {
            ["client_id"] = _config["VKAuthSettings:CLIENTID"]!,
            ["client_secret"] = _config["VKAuthSettings:CLIENTSECRET"]!,
            ["redirect_uri"] = "http://localhost:3000/afterCallback",
            ["code"] = code
        };
        var uri = QueryHelpers.AddQueryString(
            VkontakteAuthenticationDefaults.TokenEndpoint,
            query);
        var res = await _client.GetAsync(uri, cancellationToken);
        var resultTokenString = await res.Content.ReadAsStringAsync(cancellationToken);
        if (resultTokenString == string.Empty)
            return BadRequest();
        var accessToken = JsonSerializer.Deserialize<VkAccessTokenDto>(resultTokenString);
        var vkUser = await _mediator.Send(
            new GetVkUserInfoQuery(accessToken!),
            cancellationToken);
        var authResult = await OAuthCallback(vkUser.Value!, cancellationToken);
        if (authResult.Successful)
        {
            return Ok(authResult.Message);
        }
        return BadRequest(authResult.Message);
    }

    // -> GetUserFromVkById
    // -> if user is null; then
    // -> Register
    // -> AddGeolocation latitude: 55.558741, longitude: 37.378847
    // -> return Login
    // -> fi
    // -> return Login
    private async Task<LoginResponseDto> OAuthCallback(VkUserDto vkUserDto, CancellationToken cancellationToken)
    {
        if (!DateTime.TryParse(vkUserDto.DateOfBirth, new CultureInfo("en-US"), out DateTime parsedDate))
        {
            parsedDate = DateTime.Parse("2.1.1999", new CultureInfo("en-US"));
        }

        var registerDto = new VkAuthDto
        {
            VkId = vkUserDto.VkId.ToString(),
            About = vkUserDto.About,
            Email = vkUserDto.Email,
            FirstName = vkUserDto.FirstName,
            Gender = vkUserDto.Gender.ToString(),
            LastName = vkUserDto.LastName,
            UserName = vkUserDto.UserName,
            DateOfBirth = parsedDate,
            PhotoUrl = vkUserDto.PhotoUrl
        };
        User? user = default;
        var userVk = await _mediator.Send(
            new GetUserFromVkByIdQuery(vkUserDto.VkId.ToString()),
            cancellationToken);
        
        if (!userVk.IsSuccess || userVk.Value is null)
        {
            var registeredUser = await _mediator.Send(
                new RegisterOAuthVkCommand(registerDto),
                cancellationToken);
            if (!registeredUser.IsSuccess)
                return new LoginResponseDto(LoginResponseStatus.Fail);
            user = await _userManager.FindByNameAsync(registerDto.UserName);
            if (user is null)
                return new LoginResponseDto(LoginResponseStatus.Fail);
            var addUserResult = await _mediator.Send(
                new AddUserToVkCommand(
                    user.Id,
                    vkUserDto.VkId.ToString()),
                cancellationToken);
            if (!addUserResult.IsSuccess)
                return new LoginResponseDto(LoginResponseStatus.Fail);
        }
        if (user is null)
            user = await _userManager.FindByNameAsync(registerDto.UserName);
        var loginResult = await _mediator.Send(
            new LoginOAuthVkCommand(user!),
            cancellationToken);
        if (!loginResult.IsSuccess)
            return new LoginResponseDto(LoginResponseStatus.Fail);
        return new LoginResponseDto(LoginResponseStatus.Ok);
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Login");
    }
}




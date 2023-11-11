using AspNet.Security.OAuth.Vkontakte;
using Contracts.Dto.Authentication.Login;
using Contracts.Dto.Vk;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _config;
    private readonly HttpClient _client;

    public LoginController(IServiceManager serviceManager, SignInManager<User> signInManager,
        IConfiguration configuration, HttpClient client)
    {
        _serviceManager = serviceManager;
        _signInManager = signInManager;
        _config = configuration;
        _client = client;
    }

    [HttpPost]
    public async Task<JsonResult> Login([FromBody] LoginRequestDto model)
    {
        return Json(await _serviceManager.AccountService.Login(model, ModelState));
    }
    
    [HttpGet("getAccessToken")]
    public async Task<IActionResult> GetAccessToken([FromQuery] string code)
    {
        var query = new Dictionary<string, string?>
        {
            ["client_id"] = _config["VKAuthSettings:CLIENTID"]!,
            ["client_secret"] = _config["VKAuthSettings:CLIENTSECRET"]!,
            ["redirect_uri"] = "http://localhost:3000/afterCallback",
            ["code"] = code
        };
        var uri = QueryHelpers.AddQueryString(VkontakteAuthenticationDefaults.TokenEndpoint, query);
        var res = await _client.GetAsync(uri);
        var resultTokenString = await res.Content.ReadAsStringAsync();
        if (resultTokenString == string.Empty)
            return BadRequest();
        var accessToken = JsonSerializer.Deserialize<VkAccessTokenDto>(resultTokenString);
        var vkUser = await _serviceManager.VkOAuthService.GetVkUserInfoAsync(accessToken!);
        var authResult = await _serviceManager.VkOAuthService.OAuthCallback(vkUser!);
        if (authResult.Successful)
        {
            return Ok(authResult.Message);
        }
        return BadRequest(authResult.Message);
    }

    

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Login");
    }
}




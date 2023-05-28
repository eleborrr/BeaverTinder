using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text.Json.Serialization;
using AspNet.Security.OAuth.Vkontakte;
using Contracts;
using Contracts.Responses.Login;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Persistence;
using Services.Abstraction;
using Persistence.Misc.Services.JwtGenerator;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IConfiguration _config;
    private readonly HttpClient _client;

    public LoginController(ApplicationDbContext ctx, IServiceManager serviceManager, SignInManager<User> signInManager,
        IJwtGenerator jwtGenerator, IConfiguration configuration, HttpClient client)
    {
        _context = ctx;
        _serviceManager = serviceManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _config = configuration;
        _client = client;
    }

    [HttpPost]
    public async Task<JsonResult> Login([FromBody] LoginDto model)
    {
        return Json(await _serviceManager.AccountService.Login(model, ModelState));
    }
    
    [HttpGet("getAccessToken")]
    public async Task<IActionResult> GetAccessToken([FromQuery] string code)
    {
        var query = new Dictionary<string, string>()
        {
            ["client_id"] = _config["VKAuthSettings:CLIENTID"],
            ["client_secret"] = _config["VKAuthSettings:CLIENTSECRET"],
            ["redirect_uri"] = "http://localhost:3000/afterCallback",
            ["code"] = code
        };
        var uri = QueryHelpers.AddQueryString(VkontakteAuthenticationDefaults.TokenEndpoint, query);
        var res = await _client.GetAsync(uri);
        var resultTokenString = await res.Content.ReadAsStringAsync();
        if (resultTokenString == null)
            return BadRequest();
        var accessToken = JsonSerializer.Deserialize<VkAccessTokenDto>(resultTokenString);
        var vkUser = await _serviceManager.VkOAuthService.GetVkUserInfoAsync(accessToken);
        var authResult = await _serviceManager.VkOAuthService.OAuthCallback(vkUser);
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




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
using Microsoft.EntityFrameworkCore;
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

    public LoginController(ApplicationDbContext ctx, IServiceManager serviceManager, SignInManager<User> signInManager,
        IJwtGenerator jwtGenerator)
    {
        _context = ctx;
        _serviceManager = serviceManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    [HttpPost]
    public async Task<JsonResult> Login([FromBody] LoginDto model)
    {
        return Json(await _serviceManager.AccountService.Login(model, ModelState));
    }

    [HttpGet("oauth")]
    public async Task<IActionResult> OAuth()
    {
        var redirectUrl = Url.Action("OAuthCallback", "Login", null, Request.Scheme);
        var properties = new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        };
        var res = Challenge(properties, VkontakteAuthenticationDefaults.AuthenticationScheme);
        //await res.ExecuteResultAsync(new ActionContext() {HttpContext = HttpContext});
        return res;
        /*return Redirect()*/
    }
    
    //TODO обработка скрытой даты рождения
    [HttpGet("oauthcallback")]
    public async Task<IActionResult> OAuthCallback()
    {
        /*var client = new HttpClient();*/
        var result = await HttpContext.AuthenticateAsync(VkontakteAuthenticationDefaults.AuthenticationScheme);
        if (result.Succeeded)
        {
            var vkId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var name = result.Principal.FindFirstValue(ClaimTypes.GivenName);
            var surname = result.Principal.FindFirstValue(ClaimTypes.Surname);
            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var sex = result.Principal.FindFirstValue(ClaimTypes.Gender);
            var about = result.Principal.FindFirstValue("about");
            var nickname = result.Principal.FindFirstValue(ClaimTypes.Name);
            var bdate = result.Principal.FindFirstValue(ClaimTypes.DateOfBirth);
            DateTime parsedDate;
            if (!DateTime.TryParse(bdate, out parsedDate))
            {
                parsedDate = DateTime.Parse("2.1.1999");
            }

            var registerDto = new VkAuthDto()
            {
                VkId = vkId,
                About = about,
                Email = email,
                FirstName = name,
                Gender = sex,
                LastName = surname,
                UserName = nickname,
                DateOfBirth = parsedDate
            };
            var authRes = await _serviceManager.VkOAuthService.AuthAsync(registerDto);
            return Ok(authRes.Message);
        }

        return Unauthorized(result.Failure.Message);
    }

    [HttpGet("getAccessToken")]
    public async Task<IActionResult> GetAccessToken([FromQuery] string code)
    {
        var _client = new HttpClient();
        var accessToken = JsonSerializer.Serialize(new OAuthAccessTokenDto()
        {
            ClientId = "51656119",
            ClientSecret = "xXuSz8DrSkgaWkdQls9J",
            Code = code
        });
        var res = await _client.PostAsync(VkontakteAuthenticationDefaults.TokenEndpoint, new StringContent(accessToken, null, "application/json"));
        var resultTokenString = await res.Content.ReadAsStringAsync();
        if (resultTokenString == null)
            return BadRequest();
        var accessTokenObj = JsonSerializer.Deserialize<AccessToken>(resultTokenString);
        
        
        /*var userInfo = await GetUserInfo(accessTokenObj.Token); */
        return Ok();
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Login");
    }
}

public class OAuthAccessTokenDto
{
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }
        
    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; }
        
    [JsonPropertyName("code")]
    public string Code { get; set; }
}

public class AccessToken
{
    [JsonPropertyName("access_token")]
    public string Token { get; set; }
    [JsonPropertyName("expires_in")]
    public string Expires { get; set; }
    [JsonPropertyName("user_id")]
    public string Id { get; set; }
}

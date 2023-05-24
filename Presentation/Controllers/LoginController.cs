using System.Security.Claims;
using Contracts;
using Contracts.Responses.Login;
using Digillect.AspNetCore.Authentication.VKontakte;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Persistence;
using Services.Abstraction;
using Persistence.Misc.Services.JwtGenerator;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginController(ApplicationDbContext ctx, IServiceManager serviceManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
    {
        _context = ctx;
        _serviceManager = serviceManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    //TODO перенести логику в сервис
    [HttpPost]
    public async Task<JsonResult> Login([FromBody]LoginDto model)
    {
        //TODO сделать чек?
        return Json(await _serviceManager.AccountService.Login(model, ModelState));
    }
    
    [HttpGet("oauth")]
    public async Task<IActionResult> GetOAuthToken()
    {
        var props = new AuthenticationProperties()
        {
            RedirectUri = "https://localhost:7015/login/oauthcallback"
        };
        return Challenge(props, "VK");
    }
    
    //TODO перенести логику в сервис
    //TODO тут вроде надо залогинитться с нашим методом логин, но там нужны данные пользовтеля, так что либо нужен метод с авторизацией без LoginModel, либо брать какие то данные с VK и по ним находить
    [HttpGet("oauthcallback")]
    public async Task<IActionResult> OAuthCallback()
    {
        var result = await HttpContext.AuthenticateAsync("VK");
        if (result.Succeeded)
        {
            //_serviceManager.AccountService.Login();
            return Ok("залогинился)");
        }
        else
        {
            // Аутентификация не удалась
            Unauthorized("не залогнинился(");
        }

        return Ok();
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Login");
    }
}
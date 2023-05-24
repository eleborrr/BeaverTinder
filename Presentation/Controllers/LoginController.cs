using System.Security.Claims;
using Contracts;
using Contracts.Responses.Login;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Login");
    }
}
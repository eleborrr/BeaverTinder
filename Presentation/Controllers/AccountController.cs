using System.Text;
using System.Web;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Services.Abstraction;
using Services.Abstraction.TwoFA;

namespace Presentation.Controllers;


//TODO методы для изменения информации об аккаунте


[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITwoFAService _faService;
    
    public AccountController(IServiceManager serviceManager, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _faService = serviceManager.TwoFaService;
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
}
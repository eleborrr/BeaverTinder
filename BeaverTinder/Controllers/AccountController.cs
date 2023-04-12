using System.Text;
using System.Web;
using BeaverTinder.Models;
using BeaverTinder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITwoFAService _faService;
    
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITwoFAService twoFaService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _faService = twoFaService;
    }

    
    [HttpGet("/all")]
    public List<User> GetAllUsers()  //List<User>
    {
        return _userManager.Users.ToList();
    }

    [HttpGet("/empty")]
    [Authorize]
    public IActionResult EmptyPage()
    {
        return View("../EmptyPage");
    }

   
}
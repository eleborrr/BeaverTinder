using BeaverTinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    // TEST DATA
    [Authorize]
    [HttpGet("/all")]
    public List<User> GetAllUsers()  //List<User>
    {
        return _userManager.Users.ToList();
        // return "all users";
    }
    
    [Authorize]
    [HttpGet("/1")]
    public string GetUser()  //List<User>
    {
        return "1 user";
    }
    
    [Authorize(Policy = "OnlyMapSub")]
    [HttpGet("/map")]
    public string GetMapSubPage()  //List<User>
    {
        return "map sub page";
    }
}
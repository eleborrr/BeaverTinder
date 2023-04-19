using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BeaverSearchController: Controller
{
    private readonly UserManager<User> _userManager;
    
    public BeaverSearchController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    // [HttpGet]
    // public IActionResult Search()
    // {
    //     return View("../Search/Search");
    // }
    //
    // [HttpGet]
    // public IActionResult GetRandom()
    // {
    //     return View("../Search/Search");
    // }
    //
    // [HttpPost("/like")]
    // public async void Like()
    // {
    //     _userManager.User
    // }
    //
    // [HttpPost("/dislike")]
    // public async void DisLike()
    // {
    //     _userManager.User
    // }
}
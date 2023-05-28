using Contracts;
using Contracts.Responses;
using Contracts.Responses.Registration;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Services.Abstraction.Geolocation;
using Services.Abstraction.TwoFA;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly UserManager<User> _userManager;
    public RegistrationController(IServiceManager serviceManager, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _serviceManager = serviceManager;
    }

    [HttpPost]
    public async Task<JsonResult> Register([FromBody]RegisterDto model)
    {
        return Json(await _serviceManager.AccountService.Register(model, ModelState));
    }
    
    // [HttpGet("/confirm")]
    // [AllowAnonymous]
    // public async Task<IActionResult> ConfirmEmail([FromQuery]string userEmail, [FromQuery]string token)
    // {
    //     if (userEmail == null || token == null)
    //     {
    //         return View("../EmptyPage");
    //     }
    //
    //     var res = await _faService.ConfirmEmailAsync(userEmail, token);
    //     if (res.Succeeded)
    //         return View("../Succes");
    //     else
    //         return View("../EmptyPage");
    // }
}
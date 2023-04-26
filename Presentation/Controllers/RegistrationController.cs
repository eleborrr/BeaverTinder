using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Services.Abstraction.Geolocation;
using Services.Abstraction.TwoFA;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IGeolocationService _geolocationService;
    private readonly ITwoFAService _faService;
    public RegistrationController(IServiceManager serviceManager, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _faService = serviceManager.TwoFaService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody]RegisterDto model)
    {
        // TODO перенести в сервис
        if (ModelState.IsValid)
        {
            var user = new User
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                UserName = model.UserName,
                Email = model.Email,
                Gender = model.Gender,
                About = model.About,
                Image = "TEST",
                
            };
            //TODO получение геолокации из дто
            
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _faService.SendConfirmationEmailAsync(user.Id);
                return Content(
                    "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
            }

            // TODO протестить что норм работает
            await _geolocationService.AddAsync(userId: _userManager.FindByEmailAsync(user.Email).Id,
                Latutide: 55.47, // geolocation from dto!
                Longtitude: 49.6);
            

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("error_message", error.Description);
            }
        }

        return RedirectToAction("GetAllUsers", "Account"); // TODO возвращать json?
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
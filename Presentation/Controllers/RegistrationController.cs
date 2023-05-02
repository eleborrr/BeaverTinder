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
    private readonly UserManager<User> _userManager;
    private readonly IGeolocationService _geolocationService;
    private readonly ITwoFAService _faService;
    public RegistrationController(IServiceManager serviceManager, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _faService = serviceManager.TwoFaService;
    }

    [HttpPost]
    public async Task<JsonResult> Register([FromBody]RegisterDto model)
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
                return Json(new RegisterResponseDto(RegisterResponseStatus.Ok));
                 // TODO протестить что норм работает
            await _geolocationService.AddAsync(userId: _userManager.FindByEmailAsync(user.Email).Id,
                Latutide: 55.47, // geolocation from dto!
                Longtitude: 49.6);
            }
           
            else
            {
                return Json(new RegisterResponseDto(RegisterResponseStatus.Fail,
                    result.Errors.FirstOrDefault().Description));
            }
        }

        return Json(new RegisterResponseDto(RegisterResponseStatus.InvalidData));
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
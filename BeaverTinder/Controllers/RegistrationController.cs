using System.Text;
using System.Web;
using BeaverTinder.Models;
using BeaverTinder.Services;
using DogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEmailServiceInterface _emailService;
    private readonly ITwoFAService _faService;
    public RegistrationController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailServiceInterface emailService, ITwoFAService faService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _faService = faService;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm]RegisterViewModel model)
    {
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
                Image = "TEST"
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // var token = await _faService.GetConfirmationToken(user);
                // await ConfirmEmail(userEmail: user.Email, token: token);
                // var res = _faService.ConfirmEmailAsync(user.Email, token);
                await _faService.SendConfirmationEmailAsync(user);
                return Content(
                    "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("error_message", error.Description);
            }
        }

        return View(model);
    }
    
    [HttpGet("/confirm")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail([FromQuery]string userEmail, [FromQuery]string token)
    {
        if (userEmail == null || token == null)
        {
            return View("../EmptyPage");
        }
        

        // token = token.Replace(" ", "+");
        var res = await _faService.ConfirmEmailAsync(userEmail, token);
        if (res.Succeeded)
            return View("../Succes");
        else
            return View("../EmptyPage");
    }
    
}
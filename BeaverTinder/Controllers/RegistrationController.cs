using BeaverTinder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public RegistrationController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("GetAllUsers", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("error_message", error.Description);
            }
        }

        return RedirectToAction("GetAllUsers", "Account");
    }
}
using System.Security.Claims;
using BeaverTinder.DataBase;
using BeaverTinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;


[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly dbContext _context;

    public LoginController(dbContext ctx, SignInManager<User> signInManager)
    {
        _context = ctx;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm]LoginViewModel model)
    {
        bool rememberMe = false;
        if (Request.Form.ContainsKey("RememberMe"))
        {
            bool.TryParse(Request.Form["RememberMe"], out rememberMe);
        }
        model.RememberMe = rememberMe;
        
        // УЕБАНСКАЯ ХУЙНЯ ЛОМАЕТСЯ ИЗ ЗА ТОГО ЧТО emailconfirmed должен быть true!!!!! 
        if (ModelState.IsValid)
        {
            User? signedUser = await _signInManager.UserManager.FindByNameAsync(model.UserName);
            var a = await _signInManager.CheckPasswordSignInAsync(signedUser, model.Password, false);
            Console.WriteLine(signedUser.UserName);
            Console.WriteLine(signedUser.PasswordHash);
            Console.WriteLine(a);
            var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, model.Password, false, lockoutOnFailure: false);
            // var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("GetAllUsers", "Account");
            }

            ModelState.AddModelError("error_message", "Invalid login attempt.");
        }

        return View(model);
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Login");
    }
}
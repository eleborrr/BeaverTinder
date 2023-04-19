using System.Security.Claims;
using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeaverTinder.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;


[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly DbContext _context;

    public LoginController(DbContext ctx, SignInManager<User> signInManager)
    {
        _context = ctx;
        _signInManager = signInManager;
    }

    // [HttpGet]
    // public IActionResult Login(string? returnUrl = null)
    // {
    //     return View(new LoginDto { ReturnUrl = returnUrl });
    // }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody]LoginDto model)
    {
        /*bool rememberMe = false;
        /*if (Request.Form.ContainsKey("RememberMe"))
        {
            bool.TryParse(Request.Form["RememberMe"], out rememberMe);
        }#1#
        model.RememberMe = rememberMe;*/
        
        if (ModelState.IsValid)
        {
            User? signedUser = await _signInManager.UserManager.FindByNameAsync(model.UserName);
            var a = await _signInManager.CheckPasswordSignInAsync(signedUser, model.Password, false);
            var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, model.Password, false, lockoutOnFailure: false);

            
            if (result.Succeeded)
            {
                if (await _signInManager.UserManager.IsInRoleAsync(signedUser, "Admin"))
                    await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim(ClaimTypes.Role, "Admin"));

                return Ok("Success");
                return RedirectToAction("GetAllUsers", "Account");
            }

            ModelState.AddModelError("error_message", "Invalid login attempt.");
        }

        return Ok(model);
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        //Refactor?
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Login");
    }
}
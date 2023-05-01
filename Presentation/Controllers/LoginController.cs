using System.Security.Claims;
using Contracts;
using Contracts.Responses.Login;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;

    public LoginController(ApplicationDbContext ctx, SignInManager<User> signInManager)
    {
        _context = ctx;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<JsonResult> Login([FromBody]LoginDto model)
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
            var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, model.Password, false, lockoutOnFailure: false);

            
            if (result.Succeeded)
            {
                if (await _signInManager.UserManager.IsInRoleAsync(signedUser, "Admin"))
                    await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim(ClaimTypes.Role, "Admin"));

                return Json(new LoginResponseDto(LoginResponseStatus.Ok));
            }

            // ModelState.AddModelError("error_message", "Invalid login attempt.");
        }

        return Json(new LoginResponseDto(LoginResponseStatus.Fail));
    }

    [HttpGet("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Login");
    }
}
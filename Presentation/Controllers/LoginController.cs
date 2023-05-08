using System.Security.Claims;
using Contracts;
using Contracts.Responses.Login;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Misc.Services.JwtGenerator;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginController(ApplicationDbContext ctx, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
    {
        _context = ctx;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    //TODO перенести логику в сервис
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
            if (signedUser is null)
                return Json(new LoginResponseDto(LoginResponseStatus.Fail, "user not found"));
            var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, model.Password, false, lockoutOnFailure: false);

            
            if (result.Succeeded)
            {
                await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim("Id", signedUser.Id));
                if (await _signInManager.UserManager.IsInRoleAsync(signedUser, "Admin"))
                    await _signInManager.UserManager.AddClaimAsync(signedUser, new Claim(ClaimTypes.Role, "Admin"));

                return Json(new LoginResponseDto(LoginResponseStatus.Ok, await _jwtGenerator.GenerateJwtToken(signedUser.Id)));
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
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
    private readonly dbContext _context;

    public LoginController(dbContext ctx)
    {
        _context = ctx;
    }

    [HttpPost("login")]
    public async Task<IResult> Login()
    {
        //у пароля ток хэш??
        var form = HttpContext.Request.Form;
        if (!form.ContainsKey("email") || !form.ContainsKey("password"))
            return Results.BadRequest("Логин и/или пароль не установлены");
        string email = form["email"];
        string password = form["password"];
        User? user = _context.Users.FirstOrDefault(p => p.Email == email);
        if (user is null) return Results.Unauthorized();
        var claims = new List<Claim>
        { 
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync(claimsPrincipal);
        return Results.Redirect("/");
        

    }
    
    
}
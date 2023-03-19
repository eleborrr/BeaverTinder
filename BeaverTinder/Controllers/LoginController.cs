using System.Security.Claims;
using BeaverTinder.DataBase;
using BeaverTinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

public class LoginController : Controller
{
    [Authorize]
    [HttpPost("login")]
    public IResult Login()
    {
        //у пароля ток хэш??
        var form = HttpContext.Request.Form;
        if (!form.ContainsKey("email") || !form.ContainsKey("password"))
            return Results.BadRequest("Логин и/или пароль не установлены");
        string email = form["email"];
        //string password = form["password"];
        User? user = dbContext.Users.FirstOrDefault(p => p.Email == email);
        // если пользователь не найден, отправляем статусный код 401
        if (user is null) return Results.Unauthorized();
        var claims = new List<Claim>
        { 
            //new Claim(ClaimTypes.Role, user.RoleId), в юзере паходу нужен string role
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        

    }
    
    
}
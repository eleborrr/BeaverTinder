using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;


[ApiController]
[Route("[controller]")]
public class LoginController: ControllerBase
{
    [HttpGet]
    public void Login()
    {
        var inputLogin = ""; //
        var inputPassword = ""; // 
    }
}
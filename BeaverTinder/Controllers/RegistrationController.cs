using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    [HttpGet]
    public void GetRegister()
    {
        //снова фронт с беком
    }

    [HttpPost]
    public IResult Register()
    {
        var context = HttpContext;
        var form = context.Request.Form;
        //ну и дальш хз как указывать при регистрации какая роль
        return Results.Empty; //затычка
    }
}
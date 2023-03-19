using BeaverTinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    [HttpGet]
    public void GetAllUsers()  //List<User>
    {
        //я хз зачем ето и кто ето может смотреть
    }
}
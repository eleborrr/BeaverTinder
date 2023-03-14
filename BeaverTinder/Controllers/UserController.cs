using BeaverTinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController
{
    [HttpGet]
    public void GetAllUsers()  //List<User>
    {
        
    }
}
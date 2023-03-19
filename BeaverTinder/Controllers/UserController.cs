using BeaverTinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController
{
    [Authorize]
    [HttpGet("/all")]
    public string GetAllUsers()  //List<User>
    {
        return "all users";
    }
    
    [Authorize]
    [HttpGet("/1")]
    public string GetUser()  //List<User>
    {
        return "1 user";
    }
    
    [Authorize(Policy = "OnlyMapSub")]
    [HttpGet("/map")]
    public string GetMapSubPage()  //List<User>
    {
        return "map sub page";
    }
}
using BeaverTinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Controllers;


[ApiController]
[Route("[conroller]")]
public class UserController
{
    [HttpGet]
    public List<User> GetAllUsers()
    {
        
    }
}
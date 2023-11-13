using BeaverTinder.Application.Dto.Authentication.Register;
using BeaverTinder.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    private readonly IServiceManager _serviceManager;
    public RegistrationController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    public async Task<JsonResult> Register([FromBody]RegisterRequestDto model)
    {
        return Json(await _serviceManager.AccountService.Register(model, ModelState));
    }
    
}
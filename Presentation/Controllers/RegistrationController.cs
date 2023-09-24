using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation.Controllers;

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
    public async Task<JsonResult> Register([FromBody]RegisterDto model)
    {
        return Json(await _serviceManager.AccountService.Register(model, ModelState));
    }
    
}
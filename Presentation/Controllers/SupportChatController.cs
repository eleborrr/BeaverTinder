using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;


namespace Presentation.Controllers;

[ApiController]
public class SupportChatController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;

    public SupportChatController(IRepositoryManager repositoryManager, IServiceManager serviceManager, UserManager<User> userManager)
    {
        _serviceManager = serviceManager;
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }
    
    [Authorize]
    [HttpGet("history")]
    public async Task<IActionResult> GetChatHistory([FromQuery] string secondUserId)
    {
        var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
        var history = await _serviceManager.SupportChatService.GetChatHistory(user!.Id, secondUserId);
        return Ok(history);
    }
}
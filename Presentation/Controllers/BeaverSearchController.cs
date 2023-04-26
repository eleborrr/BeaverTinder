using Contracts;
using Contracts.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize]
public class BeaverSearchController: Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;
    
    public BeaverSearchController(UserManager<User> userManager, IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<User?> Search([FromBody] SearchDto searchDto)
    {
        var user = await _userManager.FindByIdAsync(searchDto.UserId);
        return await _serviceManager.FindBeaverService.GetNextBeaver(user);
        // return View("../Search/Search");
    }
    //
    // [HttpGet]
    // public IActionResult GetRandom()
    // {
    //     return View("../Search/Search");
    // }
    //
    [HttpPost("/like")]
    public async Task Like([FromBody] LikeViewModel likeViewModel)
    {
        await _serviceManager.FindBeaverService.AddSympathy(likeViewModel.UserId, likeViewModel.LikedUserId, sympathy:likeViewModel.Sympathy);
    }
    //
    [HttpPost("/dislike")]
    public async void DisLike([FromBody] LikeViewModel likeViewModel)
    {
        await _serviceManager.FindBeaverService.AddSympathy(likeViewModel.UserId, likeViewModel.LikedUserId, sympathy:likeViewModel.Sympathy);
    }
}
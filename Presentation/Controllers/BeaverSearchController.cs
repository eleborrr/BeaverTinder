﻿using Contracts;
using Contracts.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BeaverSearchController: Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IServiceManager _serviceManager;
    
    public BeaverSearchController(UserManager<User> userManager, IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
        _userManager = userManager;
    }

    //TODO: isSerching == false?? change searching algorithm
    [HttpGet]
    public async Task<JsonResult> Search()
    {
        var u = User.Identity.Name;
        var user = await _userManager.FindByNameAsync(u);
        return Json(await _serviceManager.FindBeaverService.GetNextBeaver(user));
    }
 
    
    //TODO: тут тоже с гонками все норм брат да(я постараюсь на фронте избежать но не обещаю(мб и обещаю))
    [HttpPost("/like")]
    public async Task Like([FromBody]  LikeViewModel likeViewModel)
    {
        var u = User.Identity.Name;
        var user = await _userManager.FindByNameAsync(u);
        await _serviceManager.FindBeaverService.AddSympathy(user.Id, likeViewModel.LikedUserId, sympathy:true);
    }
    //
    [HttpPost("/dislike")]
    public async void DisLike([FromBody] LikeViewModel likeViewModel)
    {
        var u = User.Identity.Name;
        var user = await _userManager.FindByNameAsync(u);
        await _serviceManager.FindBeaverService.AddSympathy(user.Id, likeViewModel.LikedUserId, sympathy:false);
    }
}
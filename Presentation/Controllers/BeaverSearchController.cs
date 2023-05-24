using System.Security.Claims;
using Contracts;
using Contracts.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        var user = await GetUserFromJwt();
        var userEntity = await _serviceManager.FindBeaverService.GetNextBeaver(user);
        var result = new SearchUserResultDto()
        {
            Id = userEntity.Id,
            About = userEntity.About,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            Age = DateTime.Now.Year - userEntity.DateOfBirth.Year,
            Gender = userEntity.Gender,
        };
        return Json(result);
    }
 
    
    //TODO: тут тоже с гонками все норм брат да(я постараюсь на фронте избежать но не обещаю(мб и обещаю))
    [HttpPost("/like")]
    public async Task Like([FromBody]  LikeViewModel likeViewModel)
    {
        var user = await GetUserFromJwt();
        await _serviceManager.FindBeaverService.AddSympathy(user.Id, likeViewModel.LikedUserId, sympathy:true);
    }
    //
    [HttpPost("/dislike")]
    public async Task DisLike([FromBody] LikeViewModel likeViewModel)
    {
        var user = await GetUserFromJwt();
        await _serviceManager.FindBeaverService.AddSympathy(user.Id, likeViewModel.LikedUserId, sympathy:false);
    }

    private async Task<User> GetUserFromJwt()
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == "Id");
        var user = await _userManager.FindByIdAsync(s.Value);
        if (user is null)
            throw new Exception("user not found"); //TODO перенести в exception
        return user;
    }
}
using System.Security.Claims;
using Contracts.Responses.Search;
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
    private readonly RoleManager<Role> _roleManager;

    public BeaverSearchController(UserManager<User> userManager, IServiceManager serviceManager, RoleManager<Role> roleManager)
    {
        _serviceManager = serviceManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    //TODO: isSerching == false?? change searching algorithm
    [HttpGet]
    public async Task<JsonResult> Search()
    {
        var result = await _serviceManager.FindBeaverService.GetNextBeaver(await GetUserFromJwt(), await GetRoleFromJwt());
        if (!result.Successful)
        {
            return Json(new SearchUserFailedResponse
            {
                Message = result.Message,
                Successful = result.Successful,
                StatusCode = result.StatusCode
            });
        }

        Console.WriteLine(result.DistanceInKm);
        var user = new SearchUserResultDto
        {
            Id = result.Id,
            About = result.About,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Age = DateTime.Now.Year - result.Age,
            DistanceInKm = result.DistanceInKm,
            Gender = result.Gender,
            Image = result.Image,
        };
        return Json(user);
    }

    [HttpGet("/mylikes")]
    public async Task<JsonResult> Likes()
    {
        var result = await _serviceManager.FindBeaverService.GetNextBeaver(await GetUserFromJwt(), await GetRoleFromJwt());
        if (!result.Successful)
        {
            return Json(new SearchUserFailedResponse()
            {
                Message = result.Message,
                Successful = result.Successful,
                StatusCode = result.StatusCode
            });
        }
        
        var user = new SearchUserResultDto()
        {
            Id = result.Id,
            About = result.About,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Age = DateTime.Now.Year - result.Age,
            Gender = result.Gender,
            DistanceInKm = result.DistanceInKm,
            Image = result.Image,
        };
        return Json(user);
    }


    //TODO: тут тоже с гонками все норм брат да(я постараюсь на фронте избежать но не обещаю(мб и обещаю))
    [HttpPost("/like")]
    public async Task<JsonResult> Like([FromBody]  LikeViewModel likeViewModel)
    {
        return Json(await _serviceManager.FindBeaverService.
            AddSympathy(await GetUserFromJwt(), likeViewModel.LikedUserId, sympathy:true, await GetRoleFromJwt()));
    }
    
    [HttpPost("/dislike")]
    public async Task<JsonResult> DisLike([FromBody] LikeViewModel likeViewModel)
    {
        return Json(await _serviceManager.FindBeaverService.
            AddSympathy(await GetUserFromJwt(), likeViewModel.LikedUserId, sympathy:false, await GetRoleFromJwt()));
    }

    private async Task<User?> GetUserFromJwt()
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == "Id");
        var user = await _userManager.FindByIdAsync(s.Value);
        // if (user is null)
        //     throw new Exception("user not found"); //TODO перенести в exception
        return user;
    }
    
    private async Task<Role> GetRoleFromJwt()
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        var role = await _roleManager.FindByNameAsync(s.Value);
        if (role is null)
            throw new Exception("role not found"); //TODO перенести в exception
        return role;
    }
}
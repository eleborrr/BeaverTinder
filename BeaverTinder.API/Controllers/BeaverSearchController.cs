using System.Security;
using System.Security.Claims;
using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Dto.ResponsesAbstraction;
using BeaverTinder.Application.Features.FindBeaver.AddSympathy;
using BeaverTinder.Application.Features.FindBeaver.GetNextBeaver;
using BeaverTinder.Application.Features.FindBeaver.GetNextSympathy;
using BeaverTinder.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BeaverSearchController: Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMediator _mediator;

    public BeaverSearchController(UserManager<User> userManager, RoleManager<Role> roleManager, IMediator mediator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<JsonResult> Search(CancellationToken cancellationToken)
    {
        var res = await _mediator.Send(
            new GetNextBeaverQuery(await GetUserFromJwt(), await GetRoleFromJwt()),
            cancellationToken);
        var result = res.Value;
        if (!result!.Successful)
            return Json(new FailResponse(result.Successful, result.Message, result.StatusCode));

        Console.WriteLine(result.DistanceInKm);
        var user = new SearchUserResultDto
        {
            Id = result.Id,
            About = result.About,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Age = result.Age,
            DistanceInKm = result.DistanceInKm,
            Gender = result.Gender,
            Image = result.Image,
        };
        return Json(user);
    }

    [HttpGet("/mylikes")]
    public async Task<JsonResult> Likes(CancellationToken cancellationToken)
    {
        var res = await _mediator.Send(
            new GetNextSympathyQuery(await GetUserFromJwt()),
            cancellationToken);
        var result = res.Value;
        if (result is null)
            return Json(new FailResponse(false, "Something went frong...", 500));
        if (result.Successful)
            return Json(new FailResponse(result.Successful, result.Message, result.StatusCode));
        
        var user = new SearchUserResultDto
        {
            Id = result.Id,
            About = result.About,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Age = result.Age,
            Gender = result.Gender,
            DistanceInKm = result.DistanceInKm,
            Image = result.Image,
        };
        return Json(user);
    }

    [HttpPost("/like")]
    public async Task<JsonResult> Like([FromBody]  LikeRequestDto likeRequestDto, CancellationToken cancellationToken)
    {
        return Json(await _mediator.Send(
            new AddSympathyCommand(
                await GetUserFromJwt(),
                likeRequestDto.LikedUserId,
                Sympathy:true,
                await GetRoleFromJwt()),
            cancellationToken));
    }
    
    [HttpPost("/dislike")]
    public async Task<JsonResult> DisLike([FromBody] LikeRequestDto likeRequestDto, CancellationToken cancellationToken)
    {
        return Json(await _mediator.Send(
            new AddSympathyCommand(
                await GetUserFromJwt(),
                likeRequestDto.LikedUserId,
                Sympathy:false,
                await GetRoleFromJwt()),
            cancellationToken));
    }

    private async Task<User?> GetUserFromJwt()
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == "Id")!;
        var user = await _userManager.FindByIdAsync(s.Value);
        return user;
    }
    
    private async Task<Role> GetRoleFromJwt()
    {
        var s = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!;
        var role = await _roleManager.FindByNameAsync(s.Value);
        if (role is null)
            throw new SecurityException("role not found");
        return role;
    }
}
using System.Security;
using System.Security.Claims;
using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Features.FindBeaver.GetNextBeaver;
using BeaverTinder.Application.Features.FindBeaver.GetNextSympathy;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Mobile.Errors;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{
    [Authorize]
    public async Task<SearchUserResultDto> Search(ClaimsPrincipal claimsPrincipal)
    {
        var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var res = await mediator.Send(
            new GetNextBeaverQuery(await GetUserFromJwt(claimsPrincipal, scope), await GetRoleFromJwt(claimsPrincipal, scope)));
        var result = res.Value;
        if (!result!.Successful)
            throw BeaverSearchError.WithMessage(result.Message);

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
        return user;
    }
    
    [Authorize]
    public async Task<SearchUserResultDto> Likes(ClaimsPrincipal claimsPrincipal)
    {
        var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var res = await mediator.Send(
            new GetNextSympathyQuery(await GetUserFromJwt(claimsPrincipal, scope)));
        var result = res.Value;
        if (result is null)
            throw BeaverSearchError.WithMessage("Something went frong...");
        if (result.Successful)
            throw BeaverSearchError.WithMessage(result.Message);
        
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
        return user;
    }
    
    private async Task<User?> GetUserFromJwt(ClaimsPrincipal claimsPrincipal, IServiceScope scope)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var s = claimsPrincipal.FindFirst(c => c.Type == "Id");
        var user = await userManager.FindByIdAsync(s.Value);
        return user;
    }
    
    private async Task<Role> GetRoleFromJwt(ClaimsPrincipal claimsPrincipal, IServiceScope scope)
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var s = claimsPrincipal.FindFirst(c => c.Type == ClaimTypes.Role)!;
        var role = await roleManager.FindByNameAsync(s.Value);
        if (role is null)
            throw new SecurityException("role not found");
        return role;
    }
}
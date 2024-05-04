using System.Security.Claims;
using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Features.FindBeaver.GetNextBeaver;
using BeaverTinder.Application.Features.FindBeaver.GetNextSympathy;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Mobile.Errors;
using HotChocolate.Authorization;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.BeaverSearch.Queries;

[Authorize]
public class Queries
{
    private readonly IMediator _mediator;

    public Queries(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<SearchUserResultDto> Search(ClaimsPrincipal claimsPrincipal)
    {
        // var s = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!;
        var res = await _mediator.Send(
            new GetNextBeaverQuery(await GetUserFromJwt(claimsPrincipal), await GetRoleFromJwt(claimsPrincipal)));
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
    
    public async Task<SearchUserResultDto> Likes(ClaimsPrincipal claimsPrincipal)
    {
        var res = await _mediator.Send(
            new GetNextSympathyQuery(await GetUserFromJwt(claimsPrincipal)));
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
    
    private async Task<User?> GetUserFromJwt(ClaimsPrincipal claimsPrincipal)
    {
        throw new NotImplementedException();
        // var s = claimsPrincipal.FindFirst(c => c.Type == "Id");
        // var user = await _userManager.FindByIdAsync(s.Value);
        // return user;
    }
    
    private async Task<Role> GetRoleFromJwt(ClaimsPrincipal claimsPrincipal)
    {
        throw new NotImplementedException();
        var s = claimsPrincipal.FindFirst(c => c.Type == ClaimTypes.Role)!;
        // var s = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!;
        // var role = await _roleManager.FindByNameAsync(s.Value);
        // if (role is null)
        //     throw new SecurityException("role not found");
        // return role;
    }
}
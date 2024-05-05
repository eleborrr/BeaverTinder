using System.Security;
using System.Security.Claims;
using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Features.FindBeaver.AddSympathy;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Mobile.Errors;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Mobile.Graphql.Mutations;

public partial class Mutations
{
    [Authorize]
    public async Task<LikeResponseDto> Like(LikeRequestDto likeRequestDto, ClaimsPrincipal claimsPrincipal)
    {
        var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var res = await mediator.Send(
            new AddSympathyCommand(
                await GetUserFromJwt(claimsPrincipal, scope),
                likeRequestDto.LikedUserId,
                Sympathy:true,
                await GetRoleFromJwt(claimsPrincipal, scope)));
        if (!res.IsSuccess)
            throw BeaverSearchError.WithMessage(res.Error);
        return res.Value;
    }
    
    [Authorize]
    public async Task<LikeResponseDto> DisLike(LikeRequestDto likeRequestDto, ClaimsPrincipal claimsPrincipal)
    {
        var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var res = await mediator.Send(
            new AddSympathyCommand(
                await GetUserFromJwt(claimsPrincipal, scope),
                likeRequestDto.LikedUserId,
                Sympathy:false,
                await GetRoleFromJwt(claimsPrincipal, scope)));
        if (!res.IsSuccess)
            throw BeaverSearchError.WithMessage(res.Error);
        return res.Value;
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
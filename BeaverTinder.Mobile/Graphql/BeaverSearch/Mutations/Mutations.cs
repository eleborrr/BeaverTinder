using System.Security.Claims;
using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Features.FindBeaver.AddSympathy;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Mobile.Errors;
using HotChocolate.Authorization;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.BeaverSearch.Mutations;

[Authorize]
public class Mutations
{
    private readonly IMediator _mediator;

    public Mutations(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<LikeResponseDto> Like(LikeRequestDto likeRequestDto, ClaimsPrincipal claimsPrincipal)
    {
        var res = await _mediator.Send(
            new AddSympathyCommand(
                await GetUserFromJwt(claimsPrincipal),
                likeRequestDto.LikedUserId,
                Sympathy:true,
                await GetRoleFromJwt(claimsPrincipal)));
        if (!res.IsSuccess)
            throw BeaverSearchError.WithMessage(res.Error);
        return res.Value;
    }
    
    public async Task<LikeResponseDto> DisLike(LikeRequestDto likeRequestDto, ClaimsPrincipal claimsPrincipal)
    {
        var res = await _mediator.Send(
            new AddSympathyCommand(
                await GetUserFromJwt(claimsPrincipal),
                likeRequestDto.LikedUserId,
                Sympathy:false,
                await GetRoleFromJwt(claimsPrincipal)));
        if (!res.IsSuccess)
            throw BeaverSearchError.WithMessage(res.Error);
        return res.Value;
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
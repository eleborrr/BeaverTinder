using System.Security;
using System.Security.Claims;
using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Features.FindBeaver.AddSympathy;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Mobile.Errors;
using BeaverTinder.Mobile.Helpers.Jwt;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Mutations
{
    [Authorize]
    public async Task<LikeResponseDto> Like(LikeRequestDto likeRequestDto, ClaimsPrincipal claimsPrincipal)
    {
        var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var res = await mediator.Send(
            new AddSympathyCommand(
                await JwtHelper.GetUserFromJwt(claimsPrincipal, scope),
                likeRequestDto.LikedUserId,
                Sympathy:true,
                await JwtHelper.GetRoleFromJwt(claimsPrincipal, scope)));
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
                await JwtHelper.GetUserFromJwt(claimsPrincipal, scope),
                likeRequestDto.LikedUserId,
                Sympathy:false,
                await JwtHelper.GetRoleFromJwt(claimsPrincipal, scope)));
        if (!res.IsSuccess)
            throw BeaverSearchError.WithMessage(res.Error);
        return res.Value;
    }
}

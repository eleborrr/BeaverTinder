using System.Security.Claims;
using BeaverTinder.Application.Dto.Account;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Mobile.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BeaverTinder.Mobile.Graphql.Profile.Mutations;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class EditUserInfoMutation
{
    private readonly IServiceManager _serviceManager;

    public EditUserInfoMutation(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<EditUserResponseDto> EditAccount(EditUserRequestDto model, ClaimsPrincipal claimsPrincipal)
    {
        var s = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _serviceManager.UserManager.FindByIdAsync(s!);

        if (user is null)
            throw BeaverSearchError.WithMessage("User not found");
        
        var b = await _serviceManager.AccountService.EditAccount(user, model);
        return b;
    }
}
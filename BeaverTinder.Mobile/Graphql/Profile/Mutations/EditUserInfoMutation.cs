using BeaverTinder.Application.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BeaverTinder.Application.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using BeaverTinder.Mobile.Errors;
using System.Security.Claims;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Mutations
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<EditUserResponseDto> EditAccount(EditUserRequestDto model, ClaimsPrincipal claimsPrincipal)
    {
        using var scope = _scopeFactory.CreateScope();
        var serviceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
        var s = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await serviceManager.UserManager.FindByIdAsync(s!);

        if (user is null)
            throw BeaverSearchError.WithMessage("User not found");
        
        var b = await serviceManager.AccountService.EditAccount(user, model);
        return b;
    }
}
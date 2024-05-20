using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Application.Dto.Account;
using BeaverTinder.Mobile.Errors;
using HotChocolate.Authorization;
using System.Security.Claims;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Mutations
{
    [Authorize]
    public async Task<EditUserResponseDto> EditAccount(EditUserRequestDto model, HttpContext context)
    {
        using var scope = _scopeFactory.CreateScope();
        var serviceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
        var id = context.User.FindFirstValue("id")!;
        var user = await serviceManager.UserManager.FindByIdAsync(id);

        if (user is null)
            throw BeaverSearchError.WithMessage("User not found");
        
        var b = await serviceManager.AccountService.EditAccount(user, model);
        return b;
    }
}
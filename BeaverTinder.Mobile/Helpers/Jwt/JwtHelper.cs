using System.Security;
using System.Security.Claims;
using BeaverTinder.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Mobile.Helpers.Jwt;

static class JwtHelper
{
    public static async Task<User?> GetUserFromJwt(ClaimsPrincipal claimsPrincipal, IServiceScope scope)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var s = claimsPrincipal.FindFirst(c => c.Type == "Id");
        var user = await userManager.FindByIdAsync(s.Value);
        return user;
    }
    
    public static async Task<Role> GetRoleFromJwt(ClaimsPrincipal claimsPrincipal, IServiceScope scope)
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var s = claimsPrincipal.FindFirst(c => c.Type == ClaimTypes.Role)!;
        var role = await roleManager.FindByNameAsync(s.Value);
        if (role is null)
            throw new SecurityException("role not found");
        return role;
    }
}